using Geten.Core.Parsers.Script.Syntax;
using Geten.Core.Parsing;
using System;
using System.Collections.Generic;
using System.IO;

namespace Geten.Core.Parsers.Script
{
	public partial class ScriptParser : BaseParser<SyntaxKind, ScriptLexer, SyntaxNode>
	{
		protected override SyntaxNode InternalParse()
		{
			var value = ParseBlock();
			MatchToken(SyntaxKind.EOF);

			return value;
		}

		private SyntaxNode ParseAdd()
		{
			return ParseTargeted<AddItemNode>("add", "item");
		}

		private SyntaxNode ParseAskFor()
		{
			var askkeyword = MatchKeyword("ask");
			var forKeyword = MatchKeyword("for");
			var message = MatchToken(SyntaxKind.String);
			var toKeyword = MatchKeyword("to");
			var slotname = MatchToken(SyntaxKind.String);

			return new AskForInputNode(askkeyword, forKeyword, message, toKeyword, slotname);
		}

		private SyntaxNode ParseCommand()
		{
			var commandkeyword = MatchKeyword("command");
			var commandString = MatchToken(SyntaxKind.String);

			return new CommandNode(commandkeyword, commandString);
		}

		private SyntaxNode ParseDecrease()
		{
			var decreasekeyword = MatchKeyword("decrease");
			var target = MatchToken(SyntaxKind.Keyword);
			var ofkeyword = MatchKeyword("of");
			var instance = MatchToken(SyntaxKind.String);
			var bykeyword = MatchKeyword("by");
			var increaseAmount = MatchToken(SyntaxKind.Number);

			return new DecreaseNode(decreasekeyword, target, ofkeyword, instance, bykeyword, increaseAmount);
		}

		private SyntaxNode ParseDialog()
		{
			var dialogkeyword = MatchKeyword("dialog");
			var target = MatchToken(SyntaxKind.String);

			return new DialogCallNode(dialogkeyword, target);
		}

		private SyntaxNode ParseEventSubscription()
		{
			var keyword = MatchKeyword("on");
			var name = MatchToken(SyntaxKind.String);
			var body = ParseProcedureBlock();

			return new EventSubscriptionNode(keyword, name, body);
		}

		private SyntaxNode ParseInclude()
		{
			MatchKeyword("include");
			var filename = MatchToken(SyntaxKind.String);

			var p = new ScriptParser();
			var tree = p.Parse(File.ReadAllText(filename.Value.ToString()));

			return tree;
		}

		private SyntaxNode ParseIncrease()
		{
			var increasekeyword = MatchKeyword("increase");
			var target = MatchToken(SyntaxKind.Keyword);
			var ofkeyword = MatchKeyword("of");
			var instance = MatchToken(SyntaxKind.String);
			var bykeyword = MatchKeyword("by");
			var increaseAmount = MatchToken(SyntaxKind.Number);

			return new IncreaseNode(increasekeyword, target, ofkeyword, instance, bykeyword, increaseAmount);
		}

		private SyntaxNode ParseLiteral()
		{
			SyntaxNode result = null;
			switch (Current.Kind)
			{
				case SyntaxKind.String:
				case SyntaxKind.Number:
				case SyntaxKind.Boolean:
				case SyntaxKind.Keyword:
					result = new LiteralNode(Current);
					break;

				default:
					Diagnostics.ReportUnexpectedLiteral(Current.Span, Current.Kind);
					break;
			}

			NextToken();

			return result;
		}

		private IEnumerable<SyntaxNode> ParseMembers()
		{
			var members = new List<SyntaxNode>();

			while (Current.Kind != SyntaxKind.EOF)
			{
				var startToken = Current;
				var member = ParseMember();

				if (member is BlockNode bn)
				{
					members.AddRange(bn.Children);
				}
				else
				{
					members.Add(member);
				}

				// If ParseMember() did not consume any tokens,
				// we need to skip the current token and continue
				// in order to avoid an infinite loop.
				//
				// We don't need to report an error, because we'll
				// already tried to parse an expression statement
				// and reported one.
				if (Current == startToken)
					NextToken();
			}

			return members;
		}

		private SyntaxNode ParseMemorySlot()
		{
			var keyword = MatchKeyword("memory");
			var slotname = MatchToken(SyntaxKind.String);
			SyntaxNode initialvalue = null;
			Token<SyntaxKind> equalsToken = null;
			if (MatchCurrentKeyword("equals"))
			{
				equalsToken = MatchKeyword("equals");
				initialvalue = ParseLiteral(); // replace with ParseValue()
			}

			return new MemorySlotDefinition(keyword, slotname, equalsToken, initialvalue);
		}

		private SyntaxNode ParsePlay()
		{
			var playkeyword = MatchKeyword("play");
			var target = MatchToken(SyntaxKind.String);
			Token<SyntaxKind> inkeyword = null;
			Token<SyntaxKind> loop = null;

			if (MatchNextKeyword("in"))
			{
				inkeyword = MatchKeyword("in");
				loop = MatchKeyword("loop");
			}

			return new PlayNode(playkeyword, target, inkeyword, loop);
		}

		private BlockNode ParseProcedureBlock()
		{
			var members = new List<SyntaxNode>();

			while (Current.Kind != SyntaxKind.EndToken && Current.Kind != SyntaxKind.EOF)
			{
				members.Add(ParseMember());
			}

			MatchToken(SyntaxKind.EndToken);

			return new BlockNode(members);
		}

		private (Token<SyntaxKind> name, SyntaxNode value) ParseProperty()
		{
			var name = MatchToken(SyntaxKind.Keyword);
			var value = ParseLiteral();

			return (name, value);
		}

		private PropertyList ParsePropertyList()
		{
			var result = new PropertyList();

			var parseNextArgument = true;
			while (parseNextArgument &&
				   Current.Kind != SyntaxKind.EndToken &&
				   Current.Kind != SyntaxKind.EOF)
			{
				var (name, value) = ParseProperty();
				result.Add(name, value);

				if (!AcceptKeyword("and", out var andToken))
				{
					parseNextArgument = false;
				}
			}

			MatchToken(SyntaxKind.EndToken);
			return result;
		}

		private T ParsePropertyOnly<T>(string keyword)
			where T : PropertyOnlyBasedCommand
		{
			var keywordToken = MatchKeyword(keyword);
			var nameToken = MatchToken(SyntaxKind.String);
			var withToken = MatchKeyword("with");
			var properties = ParsePropertyList();
			var body = new BlockNode(null);

			if (Current.Kind != SyntaxKind.EndToken && Current.Kind != SyntaxKind.EOF) //optional procedure block
			{
				body = ParseProcedureBlock();
			}

			var result = (T)Activator.CreateInstance(typeof(T), keywordToken, nameToken, withToken, properties, body);
			MatchToken(SyntaxKind.EndToken);

			return result;
		}

		private SyntaxNode ParseRemove()
		{
			return ParseTargeted<RemoveItemNode>("remove", "item");
		}

		private SyntaxNode ParseSetProperty()
		{
			var setPropertykeyword = MatchKeyword("setProperty");
			Token<SyntaxKind> target = null;
			Token<SyntaxKind> ofKeyword = null;
			if (MatchNextKeyword("of"))
			{
				ofKeyword = NextToken();
				target = MatchToken(SyntaxKind.String);
			}
			var property = MatchToken(SyntaxKind.Keyword);
			var value = ParseLiteral();

			return new SetPropertyNode(setPropertykeyword, ofKeyword, target, property, value);
		}

		private T ParseTargeted<T>(string first, string second)
															where T : TargetedNode
		{
			Token<SyntaxKind> target = null;

			var action = MatchKeyword(first);
			var argument = MatchKeyword(second);
			var name = MatchToken(SyntaxKind.String);
			Token<SyntaxKind> fromkeyword = null;

			var targetnameToken = first == "add" ? "to" : "from";
			if (MatchNextKeyword(targetnameToken))
			{
				fromkeyword = NextToken();
				target = MatchToken(SyntaxKind.String);
			}

			var result = (T)Activator.CreateInstance(typeof(T), action, argument, name, fromkeyword, target);

			return result;
		}

		private SyntaxNode ParseTell()
		{
			MatchKeyword("tell");
			var message = MatchToken(SyntaxKind.String);

			return new TellNode(message);
		}
	}
}