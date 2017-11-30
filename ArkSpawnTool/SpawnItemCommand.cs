using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArkSpawnTool
{
    class SpawnItemCommand : SpeechCommand
    {
        public static SpawnItemCommand Instance;

        static Dictionary<String, dynamic> items;

        static SpawnItemCommand()
        {
            items = JsonConvert.DeserializeObject<Dictionary<String, dynamic>>(System.IO.File.ReadAllText("config/ItemData.json"));

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append("spawn");
            gb.Append(GrammarUtils.number(), 0, 6);
            gb.Append("badass", 0, 1);
            Choices choices = new Choices();
            foreach (String key in items.Keys)
            {
                choices.Add(key);
            }
            gb.Append(choices);
            Instance = new SpawnItemCommand(gb);
        }

        private SpawnItemCommand(GrammarBuilder builder) : base(builder) { }

        public override void onRecognized(SpeechRecognitionEngine engine, SpeechRecognizedEventArgs e)
        {
            String text = e.Result.Text;

            var data = items[items.Keys.OrderByDescending(k => k.Length).Where(k => text.Contains(k)).First()];

            String bp = "";
            if (data is String)
            {
                bp = data;
            }
            else if (data is JObject)
            {
                bp = data["bp"];
            }
            else
            {
                throw new Exception("Unsupported class type " + data.GetType());
            }

            int quantity = 1;
            try
            {
                quantity = GrammarUtils.wordsToNumber(String.Join(" ", text.Split(' ').ToList().Select(word => word.Trim()).Where(word => GrammarUtils.wordToNum.ContainsKey(word))));
            }
            catch
            {
                //no numeric words
            }

            int numSpawns = 1;

            if (data is JObject && data["spawn_cap"] != null)
            {
                int spawnCount = data["spawn_cap"];
                numSpawns = quantity / spawnCount;
                quantity = spawnCount;
            }

            int quality = 1;
            if (text.Contains("badass"))
            {
                quality = 100;
            }

            MainWindow.sendCommand($"admincheat GiveItem \"{bp}\" {quantity} {quality} 0");
            if (numSpawns > 1)
            {
                Thread.Sleep(100);
                RepeatCommand.repeatPriorCommand(numSpawns - 1);
            }
        }
    }
}