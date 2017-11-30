using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;

namespace ArkSpawnTool
{
    public static class GrammarUtils
    {
        private static Choices numberChoices = new Choices();
        static GrammarUtils()
        {
            foreach (string key in wordToNum.Keys)
            {
                numberChoices.Add(key);
            }
            numberChoices.Add("and");
        }

        public static GrammarBuilder number()
        {
            return numberChoices;
        }

        public static Dictionary<string, int> wordToNum = new Dictionary<string, int>()
        {
            {"one",   1}, {"two",   2}, {"three", 3}, {"four",  4}, {"five",  5}, {"six",   6},
            {"seven", 7}, {"eight", 8}, {"nine",  9}, {"ten",  10}, {"eleven",  11}, {"twelve",   12},
            {"thirteen", 13}, {"fourteen", 14}, {"fifteen", 15}, {"sixteen", 16}, {"seventeen", 17},
            {"eighteen", 18}, {"nineteen", 19}, {"twenty", 20}, {"thirty", 30}, {"fourty", 40},
            {"fifty", 50}, {"sixty", 60}, {"seventy", 70}, {"eighty", 80}, {"ninety", 90}, {"hundred", 100},
            {"thousand", 1000}
        };

        public static int wordsToNumber(string words)
        {
            string[] tokens = words.Split(' ');
            int[] values = new int[tokens.Length];
            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];
                if (token == "and")
                {
                    continue;
                }
                values[i] = wordToNum[token];
                if (token == "hundred" || token == "thousand")
                {
                    int prev = values[i - 1];
                    values[i - 1] = -1;
                    values[i] = prev * wordToNum[token];
                }
            }

            int sum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != -1)
                {
                    sum += values[i];
                }
            }

            return sum;
        }

        public static int extractNumber(String text)
        {
            return wordsToNumber(String.Join(" ", text.Split(' ').ToList().Select(word => word.Trim()).Where(word => GrammarUtils.wordToNum.ContainsKey(word))));
        }

        public static string secondsToWords(int seconds)
        {
            TimeSpan timespan = TimeSpan.FromSeconds(seconds);
            List<string> timeunits = new List<string>();
            if (timespan.Days > 0)
            {
                timeunits.Add(timespan.Days + " days");
                timeunits.Add(timespan.Hours + " hours");
                timeunits.Add(timespan.Minutes + " minutes");
                timeunits.Add(timespan.Seconds + " seconds");
            }
            else if (timespan.Hours > 0)
            {
                timeunits.Add(timespan.Hours + " hours");
                timeunits.Add(timespan.Minutes + " minutes");
                timeunits.Add(timespan.Seconds + " seconds");
            }
            else if (timespan.Minutes > 0)
            {
                timeunits.Add(timespan.Minutes + " minutes");
                timeunits.Add(timespan.Seconds + " seconds");
            }
            else if (timespan.Seconds > 0)
            {
                timeunits.Add(timespan.Seconds + " seconds");
            }
            return String.Join(" ", timeunits);
        }
    }
}