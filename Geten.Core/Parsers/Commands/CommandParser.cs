using Geten.Core.Commands;
using Geten.Core.Parsing;
using System;
using System.Text;

//N"orth, "S"outh, "E"ast, "W"est, Up, Down, look, go, self, get, inv, drop, say, use (as well as synonyms.)

namespace Geten.Core.Parsers.Commands
{
	public class CommandParser : BaseParser<CommandKind, CommandLexer, ITextCommand>
	{
		protected override ITextCommand InternalParse()
		{
			ITextCommand command = null;
			if (Current.Kind == CommandKind.Direction)
			{
				command = ParseGoCommand();
			}
			if (Current.Kind == CommandKind.Command)
			{
				NextToken();

				switch (Peek(-1).Text)
				{
					case "go":
						command = ParseGoCommand();
						break;

					case "look":
						command = ParseLookAt();
						break;

					case "drop":
						command = ParseDrop();
						break;

					case "pickup":
						command = ParsePickup();
						break;

					case "quit":
					case "exit":
						command = ParseQuit();
						break;

					case "inv":
						command = new ShowInventoryCommand();
						break;

					case "show":
						command = ParseShowCommand();
						break;

					default:
						return null;
				}
			}
			else
			{
				Diagnostics.ReportInvalidCommand(Current.Kind);
			}

			MatchToken(CommandKind.EOF);
			return command;
		}

		private ITextCommand ParseDrop()
		{
			//drop iron sword
			var sb = new StringBuilder();
			do
			{
				var token = MatchToken(CommandKind.Identifier);
				sb.Append(token.Text).Append(' ');
			}
			while (Peek(1).Kind == CommandKind.Identifier && Peek(1).Kind != CommandKind.EOF);

			return new DropItemCommand(sb.ToString().Trim());
		}

		private ITextCommand ParseGoCommand()
		{
			Direction dir;
			if (Peek(0).Kind == CommandKind.Direction)
			{
				dir = (Direction)Peek(0).Value;
			}
			else
			{
				dir = (Direction)MatchToken(CommandKind.Direction).Value;
			}

			return new GoCommand(dir);
		}

		private ITextCommand ParseLookAt()
		{
			if (Peek(1).Kind != CommandKind.Identifier)
			{
				return new LookCommand(null);
			}
			else
			{
				var id = MatchToken(CommandKind.Identifier);

				return new LookCommand(id.Text);
			}
		}

		private ITextCommand ParsePickup()
		{
			return new PickupCommand();
		}

		private ITextCommand ParseQuit()
		{
			return new QuitCommand();
		}

		private ITextCommand ParseShowCommand()
		{
			var arg = MatchToken(CommandKind.Identifier);

			if (arg.Text == "inventory" || arg.Text == "inv")
			{
				return new ShowInventoryCommand();
			}

			return null;
		}
	}
}