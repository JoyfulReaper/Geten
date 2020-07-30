using Geten.Core.Parsing;

namespace Geten.Core.Parsers.Script.Syntax
{
	public class RoutineDefinitionNode : SyntaxNode
	{
		public RoutineDefinitionNode(Token<SyntaxKind> makeKeywordToken, Token<SyntaxKind> routineKeywordToken, Token<SyntaxKind> nameToken, Token<SyntaxKind> withToken, PropertyList argumentDefinition, BlockNode body)
		{
			MakeKeywordToken = makeKeywordToken;
			RoutineKeywordToken = routineKeywordToken;
			NameToken = nameToken;
			ArgumentDefinition = argumentDefinition;
			Body = body;
			WithToken = withToken;
		}

		public PropertyList ArgumentDefinition { get; }
		public BlockNode Body { get; }
		public Token<SyntaxKind> MakeKeywordToken { get; }
		public Token<SyntaxKind> NameToken { get; }
		public Token<SyntaxKind> RoutineKeywordToken { get; }
		public Token<SyntaxKind> WithToken { get; }

		public override void Accept(IScriptVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}