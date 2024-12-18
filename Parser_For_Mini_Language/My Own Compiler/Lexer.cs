using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Own_Compiler
{
    internal class Lexer
    {

        private static readonly Dictionary<char, SyntaxKind> _operators = new()
                 {
                    { '+', SyntaxKind.PluseToken },
                    { '-', SyntaxKind.MinusToken },
                    { '*', SyntaxKind.MultiplicationToken },
                    { '/', SyntaxKind.DivisionToken },
                    { '(', SyntaxKind.OpenParenthesisToken },
                    { ')', SyntaxKind.ClosedParenthesisToken },
                };


        private readonly string _text;
        private  int _position;
        private char Current
        {
            get { 
                if(_position >= _text.Length)
                {
                    return '\0'; //marks where a string ends
                }
                return _text[_position];   
            }
       
        }
        public Lexer(string text)
        {
            this._text = text;
        }
        private void Next()
        {
           this._position++;
        }


        public SyntaxToken NextToken()
        {

            if (Current == '\0')
            {
                return new SyntaxToken(_position, "\0", null, SyntaxKind.EndOfFile);
            }
            //Handle whitespace
            if (char.IsWhiteSpace(Current))
            {
                var start = _position;
                while (char.IsWhiteSpace(Current))
                    Next();

                var text = _text.Substring(start, _position - start);
                return new SyntaxToken(start, text, null, SyntaxKind.WhiteSpace);
            }

            //Handle digits
            if (char.IsDigit(Current))
            {
                var start = _position;
                while (char.IsDigit(Current))
                    Next();

                var text = _text.Substring(start, _position - start);
                int.TryParse(text, out var value);
                return new SyntaxToken(start, text, value, SyntaxKind.NumberToken);
            }

            //Handle operators and parentheses
            if (_operators.TryGetValue(Current, out var kind))
            {
                var token = new SyntaxToken(_position, Current.ToString(), null, kind);
                Next();
                return token;
            }

            //Handle bad tokens
            var badText = _text.Substring(_position, 1);
            _position++;
            return new SyntaxToken(_position - 1, badText, null, SyntaxKind.BadToken);
        }


    }
}

