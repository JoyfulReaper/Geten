using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
{
    public class CharacterDefinitionNode : SyntaxNode
    {
        public CharacterDefinitionNode(Token<SyntaxKind> keywordToken, Token<SyntaxKind> nameToken, Token<SyntaxKind> asToken,
            Token<SyntaxKind> asWhatToken, Token<SyntaxKind> withToken, PropertyList properties)
        {
            KeywordToken = keywordToken;
            NameToken = nameToken;
            AsToken = asToken;
            AsWhatToken = asWhatToken;
            WithToken = withToken;
            Properties = properties;
        }

        public Token<SyntaxKind> AsToken { get; }
        public Token<SyntaxKind> AsWhatToken { get; }
        public Token<SyntaxKind> KeywordToken { get; }
        public Token<SyntaxKind> NameToken { get; }
        public PropertyList Properties { get; }
        public Token<SyntaxKind> WithToken { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}