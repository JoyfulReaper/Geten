using Geten.Core.Parsing.Diagnostics;
using Geten.Core.Parsing.Text;
using System;
using System.Collections.Immutable;

namespace Geten.Core.Parsing
{
    public abstract class BaseParser<TokenType, LexerType, ReturnType>
        where TokenType : struct, IComparable
        where LexerType : BaseLexer<TokenType>
    {
        protected int _position;
        protected ImmutableArray<Token<TokenType>> _tokens;
        public Token<TokenType> Current => Peek(0);
        public DiagnosticBag Diagnostics { get; } = new DiagnosticBag();

        public Token<TokenType> MatchToken(TokenType kind)
        {
            if (Current.Kind.CompareTo(kind) == 0)
                return NextToken();

            if (Current.Kind.CompareTo(kind) != 0) Diagnostics.ReportUnexpectedToken(Current.Span, Current.Kind, kind);

            return new Token<TokenType>(kind, Current.Position, null, null);
        }

        public Token<TokenType> NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        public ReturnType Parse(string src, string filename = "default.script")
        {
            var lexer = (LexerType)Activator.CreateInstance(typeof(LexerType), SourceText.From(src, filename));

            _tokens = lexer.GetAllTokens().ToImmutableArray();
            Diagnostics.AddRange(lexer.Diagnostics);

            return InternalParse();
        }

        public Token<TokenType> Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[^1];

            return _tokens[index];
        }

        protected abstract ReturnType InternalParse();
    }
}