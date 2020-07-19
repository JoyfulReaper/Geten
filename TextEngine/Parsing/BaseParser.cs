using System;
using System.Collections.Immutable;
using System.Linq;
using TextEngine.Parsing.Diagnostics;
using TextEngine.Parsing.Text;

namespace TextEngine.Parsing
{
    public abstract class BaseParser<TokenType, LexerType, ReturnType>
        where TokenType : struct, IComparable
        where LexerType : BaseLexer<TokenType>
    {
        protected ImmutableArray<Token<TokenType>> _tokens;
        protected int _position;

        public DiagnosticBag Diagnostics { get; } = new DiagnosticBag();

        public Token<TokenType> Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[^1];

            return _tokens[index];
        }

        public Token<TokenType> Current => Peek(0);

        public Token<TokenType> NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        public Token<TokenType> MatchToken(TokenType kind)
        {
            if (Current.Kind.CompareTo(kind) == 0)
                return NextToken();

            if (Current.Kind.CompareTo(kind) != 0) Diagnostics.ReportUnexpectedToken(Current.Span, Current.Kind, kind);

            return new Token<TokenType>(kind, Current.Position, null, null);
        }

        public ReturnType Parse(string src, string filename = "default.script")
        {
            var lexer = (LexerType)Activator.CreateInstance(typeof(LexerType), SourceText.From(src, filename));
            
            _tokens = lexer.GetAllTokens().ToImmutableArray();
            Diagnostics.AddRange(lexer.Diagnostics);

            return InternalParse();
        }

        protected abstract ReturnType InternalParse();
    }
}