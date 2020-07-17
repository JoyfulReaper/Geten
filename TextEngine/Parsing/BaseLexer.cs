using System.Text;
using TextEngine.Parsing.Text;

namespace TextEngine.Parsing
{
    public abstract class BaseLexer<TokenType>
        where TokenType : struct
    {
        protected readonly SourceText _text;
        protected int _position;

        protected int _start;
        protected TokenType _kind;
        protected object _value;

        public BaseLexer(SourceText src)
        {
            _text = src;
        }

        public abstract Token<TokenType> Lex();

        protected char Current => Peek(0);

        protected char Lookahead => Peek(1);

        private char Peek(int offset)
        {
            var index = _position + offset;

            if (index >= _text.Length)
                return '\0';

            return _text[index];
        }
    }
}
