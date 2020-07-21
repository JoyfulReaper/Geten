using Geten.Core.Parsing;

namespace Geten.Parsers.Script.Syntax
{
    public class CommandNode : SyntaxNode
    {
        public CommandNode(Token<SyntaxKind> keywordToken, Token<SyntaxKind> commandToken)
        {
            KeywordToken = keywordToken;
            CommandToken = commandToken;
        }

        public Token<SyntaxKind> KeywordToken { get; }
        public Token<SyntaxKind> CommandToken { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
