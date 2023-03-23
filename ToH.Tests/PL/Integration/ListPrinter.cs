using System.Collections.Generic;
using ToH.PL;

namespace ToH.Tests.Integration
{
    internal class ListPrinter : IPrinter
    {

        private readonly List<string> _lines;

        public ListPrinter()
        {
            _lines = new List<string>();
        }

        public List<string> Lines()
        {
            return new List<string>(_lines);
        }

        public void Clear()
        {
            _lines.Clear();
        }

        public void Print(string text)
        {
            if (_lines.Count == 0)
            {
                _lines.Add("");
            }
            _lines[_lines.Count - 1] += text;
        }

        public void PrintLine(string line)
        {
            _lines.Add(line);
        }
    }
}
