using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PrintApp
{
    public class TextDispenser
    {
        int _start = 0;
        string _text = null;
        Font _fnt;
        public TextDispenser(string text, Font fnt)
        {
            _start = 0;
            _text = text;
            _fnt = fnt;
        }
        public bool DrawText(Graphics target, Graphics measurer, RectangleF r, Brush brsh)
        {
            if (r.Height < _fnt.Height)
                throw new ArgumentException("The rectangle is not tall enough to fit a single line of text inside.");
            int charsFit = 0;
            int linesFit = 0;
            int cut = 0;
            string temp = _text.Substring(_start);
            StringFormat format = new StringFormat(StringFormatFlags.FitBlackBox | StringFormatFlags.LineLimit);
            //measure how much of the string we can fit into the rectangle
            measurer.MeasureString(temp, _fnt, r.Size, format, out charsFit, out linesFit);
            cut = BreakText(temp, charsFit);
            if (cut != charsFit)
                temp = temp.Substring(0, cut);
            bool h = true;
            h &= true;
            target.DrawString(temp.Trim(' '), _fnt, brsh, r, format);
            _start += cut;
            if (_start == _text.Length)
            {
                _start = 0; //reset the location so we can repeat the document
                return true; //finished printing
            }
            else
                return false;
        }
        private static int BreakText(string text, int approx)
        {
            if (approx == 0)
                throw new ArgumentException();
            if (approx < text.Length)
            {
                //are we in the middle of a word?
                if (char.IsLetterOrDigit(text[approx]) && char.IsLetterOrDigit(text[approx - 1]))
                {
                    int temp = text.LastIndexOf(' ', approx, approx + 1);
                    if (temp >= 0)
                        return temp;
                }
            }
            return approx;
        }

    }
}
