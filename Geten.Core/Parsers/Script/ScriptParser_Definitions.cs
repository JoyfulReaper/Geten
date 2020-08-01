using Geten.Core.Crafting;
using Geten.Core.Parsers.Script.Syntax;
using Geten.Core.Parsing;
using System.Collections.Generic;
using System.Linq;

namespace Geten.Core.Parsers.Script
{
	public partial class ScriptParser
	{
		private SyntaxNode ParseCharacter()
		{
			var characterKeyword = MatchKeyword("character");
			var name = MatchToken(SyntaxKind.String);

			Token<SyntaxKind> asKeyword = null;
			Token<SyntaxKind> asWhat = null;

			if (MatchNextKeyword("as"))
			{
				asKeyword = MatchKeyword("as");
				asWhat = MatchToken(SyntaxKind.Keyword);

				if (asWhat.Text != "npc" && asWhat.Text != "player")
				{
					Diagnostics.ReportUnexpectedKeyword(Current.Span, asWhat, "npc or player");
				}
			}
			else
			{
				asWhat = new Token<SyntaxKind>(SyntaxKind.Keyword, name.Position + 2, "npc", null);
			}

			var withToken = MatchKeyword("with");
			var properties = ParsePropertyList();
			var optEvent = ParseOptionalEvent(); //ToDo:may allow multiple event subscriptions?

			var endToken = MatchToken(SyntaxKind.EndToken);

			return new CharacterDefinitionNode(characterKeyword, name, asKeyword, asWhat, withToken, properties, optEvent);
		}

		private Ingredients ParseIngredients()
		{
			var result = new Ingredients();
			var parseNextIngredient = true;
			var ingredientsKeyword = MatchToken(SyntaxKind.Keyword);

			if (ingredientsKeyword.Text != "ingredients")
				Diagnostics.ReportUnexpectedKeyword(Current.Span, ingredientsKeyword, "ingredients");

			while (parseNextIngredient &&
				Current.Kind != SyntaxKind.EndToken &&
				Current.Kind != SyntaxKind.EOF)
			{
				var numberRequired = MatchToken(SyntaxKind.Number);
				var ofKeyword = MatchKeyword("of");
				var itemRequire = MatchToken(SyntaxKind.String);

				result.Add(itemRequire.Text, (int)numberRequired.Value);

				if (!AcceptKeyword("and", out var andToken))
				{
					parseNextIngredient = false;
					MatchToken(SyntaxKind.EndToken);
				}
			}
			return result;
		}

		private Optional<SyntaxNode> ParseOptionalEvent()
		{
			if (MatchCurrentKeyword("on"))
			{
				return Optional.Some(ParseEventSubscription());
			}

			return Optional.None<SyntaxNode>();
		}

		private SyntaxNode ParseRecipe()
		{
			var recipeKeywordToken = MatchKeyword("recipe");
			var nameToken = MatchToken(SyntaxKind.String);
			var willKeywordToken = MatchKeyword("will");
			var craftKeywordToken = MatchKeyword("craft");
			var quantityToken = MatchToken(SyntaxKind.Number);
			var ofKeywordToken = MatchKeyword("of");
			var ouputToken = MatchToken(SyntaxKind.String);

			var ingredients = ParseIngredients();
			var endToken = MatchToken(SyntaxKind.EndToken);

			return new RecipeDefinitionNode(recipeKeywordToken, nameToken, willKeywordToken, craftKeywordToken, quantityToken, ofKeywordToken, ouputToken, ingredients, endToken);
		}

		private SyntaxNode ParseRecipeBook()
		{
			var recipeKeyword = MatchKeyword("recipebook");
			var name = MatchToken(SyntaxKind.String);
			var members = ParseRecipes();

			if (!members.Any())
			{
				Diagnostics.ReportNoRecipesInBook(name.Span, name.Value.ToString());
			}

			var endToken = MatchToken(SyntaxKind.EndToken);

			return new RecipeBookDefinition(recipeKeyword, name, members, endToken);
		}

		private BlockNode ParseRecipes()
		{
			var recipes = new List<SyntaxNode>();

			while (Current.Kind != SyntaxKind.EOF && Current.Kind != SyntaxKind.EndToken)
			{
				var startToken = Current;
				var member = ParseRecipe();

				recipes.Add(member);

				if (Current == startToken)
					NextToken();
			}

			return new BlockNode(recipes);
		}

		private SyntaxNode ParseRoutineDefinition()
		{
			var makeKeyword = MatchKeyword("make");
			var routineKeyword = MatchKeyword("routine");
			var name = MatchToken(SyntaxKind.Keyword);
			var withKeyword = MatchKeyword("with");
			var argumentDefinitions = ParsePropertyList();

			var bodyKeyword = MatchKeyword("body");
			var body = ParseProcedureBlock();
			var end = MatchToken(SyntaxKind.EndToken);

			return new RoutineDefinitionNode(makeKeyword, routineKeyword, name, withKeyword, argumentDefinitions, body);
		}
	}
}