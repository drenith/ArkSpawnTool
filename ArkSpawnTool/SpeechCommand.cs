using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;

namespace ArkSpawnTool
{
    abstract class SpeechCommand : Grammar
    {
        public SpeechCommand(GrammarBuilder builder) : base(builder)
        {

        }
        public abstract void onRecognized(SpeechRecognitionEngine engine, SpeechRecognizedEventArgs e);
    }
}
