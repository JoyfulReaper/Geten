using TextEngine.Parsing.Text;

namespace TextEngine.Parsing
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
        public Token(TokenType kind, int position, CaseInsensitiveString text, object value)
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
        ///  The Token Text
        /// </summary>
        public CaseInsensitiveString Text { get; }

        /// <summary>
        ///  The Token Value
        /// </summary>
        public object Value { get; }

        /// <summary>
        ///  The TokenSpan
        /// </summary>
        public override TextSpan Span => new TextSpan(Position, Text?.Length ?? 0);

        public override string ToString()
        {
            return Kind + ": " + (Value ?? Text);
        }

        public override void Accept(IVisitor visitor)
        {
           
        }
    }
}