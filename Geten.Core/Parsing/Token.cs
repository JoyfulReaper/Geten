﻿using Geten.Core.Parsers.Script;
using Geten.Core.Parsing.Text;

namespace Geten.Core.Parsing
{
    /// <summary>
    ///  A class to represent a Token
    /// </summary>
    public sealed class Token<TokenType> : SyntaxNode
    {
        /// <summary>
        ///  initialize Token
        /// </summary>
        /// <param name="kind">The TokenTýpe</param>
        /// <param name="position">The startposition of the Token</param>
        /// <param name="text">The resulting Text</param>
        /// <param name="value">The Value. Can be Null</param>
        public Token(TokenType kind, int position, CaseSensisitiveString text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        /// <summary>
        ///  The TokenType
        /// </summary>
        public TokenType Kind { get; }

        /// <summary>
        ///  The Startposition
        /// </summary>
        public int Position { get; }

        /// <summary>
        ///  The TokenSpan
        /// </summary>
        public override TextSpan Span => new TextSpan(Position, Text?.Length ?? 0);

        /// <summary>
        ///  The Token Text
        /// </summary>
        public CaseSensisitiveString Text { get; }

        /// <summary>
        ///  The Token Value
        /// </summary>
        public object Value { get; }

        public override void Accept(IScriptVisitor visitor)
        {
        }

        public override string ToString()
        {
            return Kind + ": " + (Value ?? Text);
        }
    }
}