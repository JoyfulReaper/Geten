using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
{
    public class RecipeBookDefinition : SyntaxNode
    {
        public RecipeBookDefinition(Token<SyntaxKind> recipebookKeywordToken, Token<SyntaxKind> nameToken, BlockNode recipes, Token<SyntaxKind> endToken)
        {
            RecipebookKeywordToken = recipebookKeywordToken;
            NameToken = nameToken;
            EndToken = endToken;
            Recipes = recipes;
        }

        public Token<SyntaxKind> EndToken { get; }
        public Token<SyntaxKind> NameToken { get; }
        public Token<SyntaxKind> RecipebookKeywordToken { get; }
        public BlockNode Recipes { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}