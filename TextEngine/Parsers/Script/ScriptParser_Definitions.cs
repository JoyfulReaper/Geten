using Geten.Core.Crafting;
using Geten.Core.Parsing;
using Geten.Core.Parsing.Diagnostics;
using Geten.Parsers.Script.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Geten.Parsers.Script
{
    public partial class ScriptParser
    {
        private SyntaxNode ParseCharacter()
        {
            var characterKeyword = MatchKeyword("character");
            var name = MatchToken(SyntaxKind.String);
            var asKeyword = MatchKeyword("as");
            var asWhat = MatchToken(SyntaxKind.Keyword);

            if (asWhat.Text != "npc" && asWhat.Text != "player")
            {
                // Log?
                // thow exception?
                Diagnostics.ReportUnexpectedKeyword(Current.Span, asWhat, "npc or player");
            }

            var withToken = MatchKeyword("with");
            var properties = ParsePropertyList();
            var endToken = MatchToken(SyntaxKind.EndToken);

            return new CharacterDefinitionNode(characterKeyword, name, asKeyword, asWhat, withToken, properties);
        }

        private Ingredients ParseIngredients()
        {
            var result = new Ingredients();
            bool parseNextIngredient = true;
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

            return new RecipeBookDefinition(recipeKeyword, name, members, endToken); //ToDo: add members to recipebook
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
    }
}