using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkSpawnTool
{
    class RepeatCommand : SpeechCommand
    {
        public static RepeatCommand Instance;

        static RepeatCommand()
        {
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append("repeat command");
            gb.Append(GrammarUtils.number(), 0, 6);
            gb.Append("once", 0, 1);
            gb.Append("twice", 0, 1);
            gb.Append("times", 0, 1);
            Instance = new RepeatCommand(gb);
        }

        private RepeatCommand(GrammarBuilder builder) : base(builder) { }

        public override void onRecognized(SpeechRecognitionEngine engine, SpeechRecognizedEventArgs e)
        {
            String text = e.Result.Text;
            text = text.Replace("once", "one").Replace("twice", "two");
            int times = GrammarUtils.extractNumber(text);
            repeatPriorCommand(times);
        }

        public static void repeatPriorCommand(int times)
        {
            SendKeys.SendWait("{TAB}{TAB}");
            Thread.Sleep(500);
            for (int i = 0; i < times; i++)
            {
                SendKeys.SendWait("{UP}{ENTER}");
                Thread.Sleep(10);
            }
            SendKeys.SendWait("{ENTER}");
        }
    }
}