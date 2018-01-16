using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace ArkSpawnTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SpeechSynthesizer synth;
        private static SpeechRecognitionEngine engine;
        private static Dispatcher staThreadDispatcher;

        public static Boolean enabled = true;

        KeyboardHook hook = new KeyboardHook();
        Thread listeningThread;

        public MainWindow()
        {
            InitializeComponent();

            Console.SetOut(new TextBoxWriter(console));

            initializeSpeechSynth();
            initializeSpeechRecognition();

            // register the event that is fired after the key press.
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            hook.RegisterHotKey(ModifierKeys.None, Keys.Subtract);

            Console.WriteLine("To toggle: numpad subtract");
            Console.WriteLine("spawn x y - will spawn x of y item. Omitting x will default to 1");
            Console.WriteLine("summon x y - will summon dino y of level x. Omitting x will default to 150");
            Console.WriteLine("summon and tame x y - will summon dino y of level x. Omitting x will default to 150");
            Console.WriteLine("Recognition Confidence Threshold: " + Properties.Settings.Default.Confidence);

            staThreadDispatcher = this.Dispatcher;

            listeningThread = new Thread(listen);
            listeningThread.Start();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            enableCommands();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            listeningThread.Abort();
            base.OnClosing(e);
        }

        void listen()
        {
            while (true)
            {
                engine.Recognize();
            }
        }

        void initializeSpeechSynth()
        {
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SelectVoiceByHints(VoiceGender.Female);
        }

        void initializeSpeechRecognition()
        {
            engine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            engine.SetInputToDefaultAudioDevice();
            engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
        }

        void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Heard: " + e.Result.Text + " [" + e.Result.Confidence + "]");
            if (e.Result.Confidence > (Properties.Settings.Default.Confidence / 100) && "ARK: Survival Evolved".Equals(Utils.GetActiveWindowTitle()))
            {
                SpeechCommand command = (SpeechCommand)e.Result.Grammar;
                try
                {
                    command.onRecognized((SpeechRecognitionEngine)sender, e);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static void enableCommands()
        {
            synth.Speak("Enabled");
            engine.UnloadAllGrammars();
            engine.LoadGrammar(SummonDinoCommand.Instance);
            engine.LoadGrammar(SummonAndTameCommand.Instance);
            engine.LoadGrammar(SpawnArtifacts.Instance);
            engine.LoadGrammar(SpawnItemCommand.Instance);            
            engine.LoadGrammar(MiscCommands.Instance);
            engine.LoadGrammar(TeleportCommand.Instance);
            engine.LoadGrammar(RepeatCommand.Instance);
            engine.LoadGrammar(PrepareRockwell.Instance);
            enabled = true;
        }

        public static void disableCommands()
        {
            synth.Speak("Disabled");
            engine.UnloadAllGrammars();
            enabled = false;
        }

        public static void sendCommand(String command)
        {
            staThreadDispatcher.Invoke(new Action(() =>
            {
                String oldText = System.Windows.Clipboard.GetText();

                System.Windows.Clipboard.SetText(command);

                SendKeys.SendWait("{TAB}");
                Thread.Sleep(500);
                SendKeys.SendWait("^{v}{ENTER}");

                Thread.Sleep(100);

                System.Windows.Clipboard.SetText(oldText);
            }));
        }

        public static void sendMultipleCommands(List<String> commands)
        {
            foreach(String command in commands)
            {
                sendCommand(command);
            }
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (enabled)
            {
                disableCommands();
            }
            else
            {
                enableCommands();
            }
        }

        private void updateConfidence(object sender, RoutedEventArgs e)
        {
            String response = "" + Properties.Settings.Default.Confidence;
            showInputBox("Update Confidence", "New Threshold:", ref response, s =>
            {
                try
                {
                    double c = Double.Parse(s);
                    if (c < 0 || 100 < c)
                    {
                        return false;
                    }
                    Properties.Settings.Default.Confidence = c;
                    Properties.Settings.Default.Save();
                    return true;
                } catch (Exception ex)
                {
                    return false;
                }
            });
        }

        private DialogResult showInputBox(String title, String prompt, ref String value, Func<String, Boolean> accept)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = prompt;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOk.Click += new EventHandler((s, e) => {
                if (!accept.Invoke(textBox.Text))
                {
                    label.ForeColor = System.Drawing.Color.Red;
                    buttonOk.DialogResult = System.Windows.Forms.DialogResult.None;
                } else
                {
                    buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            });

            form.ClientSize = new System.Drawing.Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new System.Drawing.Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }
}
