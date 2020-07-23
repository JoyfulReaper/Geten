using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
{
    public class RecipeDefinitionNode : SyntaxNode
    {
        public RecipeDefinitionNode(Token<SyntaxKind> recipeKeywordToken, Token<SyntaxKind> nameToken, Token<SyntaxKind> willKeywordToken, Token<SyntaxKind> craftKeywordToken, Token<SyntaxKind> quantityToken, Token<SyntaxKind> ofKeywordToken, Token<SyntaxKind> ouputToken, Token<SyntaxKind> endToken)
        {
            RecipeKeywordToken = recipeKeywordToken;
            NameToken = nameToken;
            WillKeywordToken = willKeywordToken;
            CraftKeywordToken = craftKeywordToken;
            QuantityToken = quantityToken;
            OuputToken = ouputToken;
            EndToken = endToken;
        }

        public Token<SyntaxKind> CraftKeywordToken { get; }
        public Token<SyntaxKind> EndToken { get; }
        public Token<SyntaxKind> NameToken { get; }
        public Token<SyntaxKind> OuputToken { get; }
        public Token<SyntaxKind> QuantityToken { get; }
        public Token<SyntaxKind> RecipeKeywordToken { get; }
        public Token<SyntaxKind> WillKeywordToken { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}