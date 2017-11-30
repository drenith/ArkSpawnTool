using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace ArkSpawnTool
{
    class TeleportCommand : SpeechCommand
    {
        public static TeleportCommand Instance;
        static Dictionary<String, String> locations;

        static TeleportCommand()
        {
            locations = JsonConvert.DeserializeObject<Dictionary<String, String>>(System.IO.File.ReadAllText("config/TeleportData.json"));

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append("teleport");
            gb.Append("to", 0, 1);
            Choices choices = new Choices();
            foreach (String key in locations.Keys)
            {
                choices.Add(key);
            }
            gb.Append(choices);

            Instance = new TeleportCommand(gb);
        }

        public TeleportCommand(GrammarBuilder builder) : base(builder) { }

        public override void onRecognized(SpeechRecognitionEngine engine, SpeechRecognizedEventArgs e)
        {
            String text = e.Result.Text.ToLower();
            String location = null;
            foreach (String key in locations.Keys)
            {
                if (text.Contains(key.ToLower()))
                {
                    location = key;
                    break;
                }
            }

            MainWindow.sendCommand("admincheat setplayerpos " + locations[location]);
        }
    }
}