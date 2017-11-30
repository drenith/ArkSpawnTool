using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ArkSpawnTool
{
    public class TextBoxWriter : TextWriter
    {
        private TextBox textbox;
        public TextBoxWriter(TextBox textbox)
        {
            this.textbox = textbox;
        }

        public override void Write(char value)
        {
            textbox.Dispatcher.Invoke(() => {
                textbox.AppendText("" + value);
                textbox.ScrollToEnd();
            });
        }

        public override void Write(string value)
        {
            textbox.Dispatcher.Invoke(() => {
                textbox.AppendText(value);
                textbox.ScrollToEnd();
            });
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
