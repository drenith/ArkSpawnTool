using System;
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

            enableCommands();

            staThreadDispatcher = this.Dispatcher;

            listeningThread = new Thread(listen);
            listeningThread.Start();
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
            Console.WriteLine("Heard: " + e.Result.Text + " [ " + e.Result.Confidence + "]");
            if (e.Result.Confidence > 0.84 && "ARK: Survival Evolved".Equals(Utils.GetActiveWindowTitle()))
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

            /*
            engine.LoadGrammar(SpawnArtifacts.Instance);
            engine.LoadGrammar(SpawnItemCommand.Instance);            
            
            engine.LoadGrammar(MiscCommands.Instance);
            engine.LoadGrammar(TeleportCommand.Instance);
            engine.LoadGrammar(RepeatCommand.Instance);
            */
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
    }
}
