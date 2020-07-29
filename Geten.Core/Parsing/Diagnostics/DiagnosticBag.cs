using Geten.Core.Crafting;
using Geten.Core.Parsers.Commands;
using Geten.Core.Parsing.Text;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Geten.Core.Parsing.Diagnostics
{
	public sealed class DiagnosticBag : IEnumerable<Diagnostic>
	{
		private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();

		public void AddRange(DiagnosticBag diagnostics)
		{
			_diagnostics.AddRange(diagnostics._diagnostics);
		}

		public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();

		public void ReportBadCharacter(TextSpan location, char character)
		{
			var message = $"Bad character input: '{character}'.";
			Report(location, message);
		}

		public void ReportInvalidCommand(CommandKind kind)
		{
			var message = $"'{kind}' is not a valid Command";
			Report(TextSpan.FromBounds(0, 0), message);
		}

		public void ReportInvalidNumber(TextSpan span, string text)
		{
			var message = $"The number {text} isn't valid.";
			Report(span, message);
		}

		public void ReportUnexpectedToken<KindType>(TextSpan span, KindType actualKind, KindType expectedKind)
		{
			var message = $"Unexpected token <{actualKind}>, expected <{expectedKind}>.";
			Report(span, message);
		}

		public void ReportUnterminatedString(TextSpan span)
		{
			var message = "Unterminated string literal.";
			Report(span, message);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _diagnostics.GetEnumerator();
		}

		internal void ReportBadNPC(string name)
		{
			var message = $"NPC '{name}' is not valid";
			Report(TextSpan.FromBounds(0, 0), message);
		}

		internal void ReportBadPlayerCharacter(string name)
		{
			var message = $"Character '{name}' must be player or npc";
			Report(TextSpan.FromBounds(0, 0), message);
		}

		internal void ReportBadTargetInventory(string target)
		{
			var message = $"Target '{target}' is not a valid Room, NPC or ContainerItem";
			Report(TextSpan.FromBounds(0, 0), message);
		}

		internal void ReportNoRecipesInBook(TextSpan location, string book)
		{
			var message = $"The RecipeBook '{book}' has no Recipes.";
			Report(location, message);
		}

		internal void ReportUnexpectedKeyword<KindType>(TextSpan location, Token<KindType> keywordToken, string keyword)
		{
			var message = $"Expected '{keyword}' got '{keywordToken.Text}'.";
			Report(location, message);
		}

		internal void ReportUnexpectedKeyword<KindType>(TextSpan location, Token<KindType> keywordToken)
		{
			var message = $"Unexpected '{keywordToken.Text}'.";
			Report(location, message);
		}

		internal void ReportUnexpectedLiteral<KindType>(TextSpan location, KindType kind)
		{
			var message = $"Unexpected Literal '{kind}'.";
			Report(location, message);
		}

		private void Report(TextSpan span, string message)
		{
			var diagnostic = new Diagnostic(span, message);
			_diagnostics.Add(diagnostic);
		}
	}
}