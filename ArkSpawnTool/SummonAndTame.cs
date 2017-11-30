﻿using System;
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
    class SummonAndTameCommand : SpeechCommand
    {

        public static SummonAndTameCommand Instance;

        static Dictionary<String, String> items;

        static SummonAndTameCommand()
        {
            items = JsonConvert.DeserializeObject<Dictionary<String, String>>(System.IO.File.ReadAllText("config/DinoData.json"));

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append("summon and tame");
            gb.Append("level", 0, 1);
            gb.Append(GrammarUtils.number(), 0, 6);
            Choices choices = new Choices();
            foreach (String key in items.Keys)
            {
                choices.Add(key);
            }
            gb.Append(choices);
            Instance = new SummonAndTameCommand(gb);
        }

        private SummonAndTameCommand(GrammarBuilder builder) : base(builder) { }

        public override void onRecognized(SpeechRecognitionEngine engine, SpeechRecognizedEventArgs e)
        {
            String text = e.Result.Text;

            String bp = "";
            foreach (String key in items.Keys.OrderByDescending(x => x.Length))
            {
                if (text.Contains(key))
                {
                    bp = items[key];
                    break;
                }
            }

            int level = 150;
            try
            {
                level = GrammarUtils.wordsToNumber(String.Join(" ", text.Split(' ').ToList().Select(word => word.Trim()).Where(word => GrammarUtils.wordToNum.ContainsKey(word))));
            }
            catch
            {
                //no numeric words
            }

            MainWindow.sendCommand($"admincheat GMSummon \"{bpToEntityId(bp)}\" {level}");
        }

        private static String bpToEntityId(String bp)
        {
            int startIndex = bp.LastIndexOf(".") + 1;
            int endIndex = bp.LastIndexOf("'");
            return bp.Substring(startIndex, endIndex - startIndex) + "_C";
        }
    }
}
