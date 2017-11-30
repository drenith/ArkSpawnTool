using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;

namespace ArkSpawnTool
{
    class MiscCommands : SpeechCommand
    {
        public static MiscCommands Instance;

        static Dictionary<String, String> commands;

        static MiscCommands()
        {
            commands = JsonConvert.DeserializeObject<Dictionary<String, String>>(System.IO.File.ReadAllText("config/MiscCommands.json"));

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append("command");
            Choices choices = new Choices();
            foreach (String key in commands.Keys)
            {
                choices.Add(key);
            }
            gb.Append(choices);
            Instance = new MiscCommands(gb);
        }

        private MiscCommands(GrammarBuilder builder) : base(builder) { }

        public override void onRecognized(SpeechRecognitionEngine engine, SpeechRecognizedEventArgs e)
        {
            String text = e.Result.Text;

            string command = commands[commands.Keys.First(key => text.Contains(key))];

            MainWindow.sendCommand($"admincheat {command}");
        }
    }
}