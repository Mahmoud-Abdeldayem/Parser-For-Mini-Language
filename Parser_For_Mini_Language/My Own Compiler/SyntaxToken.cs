using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Own_Compiler
{
    public class SyntaxToken
    {

        public SyntaxToken(int position,string text, object value, SyntaxKind syntaxKind)
        {
            Position = position;
            Text = text;
            Value = value;
            SyntaxKind = syntaxKind;
        }

        public int Position { get; }
        public string Text { get; }
        public object Value { get; }
        public SyntaxKind SyntaxKind { get; }
    }
}
