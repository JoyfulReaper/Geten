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
				return ParseGoCommand();
			}
			if (Current.Kind == CommandKind.Command)
			{
				switch (Current.Text)
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
			//drop iron sword?
			MatchToken(CommandKind.Command);
			var sb = new StringBuilder();
			do
			{
				var token = MatchToken(CommandKind.Identifier);
				sb.Append(token.Text).Append(' ');
			}
			while (Current.Kind == CommandKind.Identifier && Current.Kind != CommandKind.EOF);

			return new DropItemCommand(sb.ToString().Trim());
		}

		private ITextCommand ParseGoCommand()
		{
			Direction dir;
			if (Current.Kind == CommandKind.Direction)
			{
				dir = (Direction)MatchToken(CommandKind.Direction).Value;
			}
			else
			{
				MatchToken(CommandKind.Command);
				dir = (Direction)MatchToken(CommandKind.Direction).Value;
			}

			return new GoCommand(dir);
		}

		private ITextCommand ParseLookAt()
		{
			MatchToken(CommandKind.Command);
			if (Current.Kind != CommandKind.Identifier)
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
			MatchToken(CommandKind.Command);
			return new PickupCommand();
		}

		private ITextCommand ParseQuit()
		{
			MatchToken(CommandKind.Command);
			return new QuitCommand();
		}

		private ITextCommand ParseShowCommand()
		{
			MatchToken(CommandKind.Command);
			var arg = MatchToken(CommandKind.Identifier);

			if (arg.Text == "inventory" || arg.Text == "inv")
			{
				return new ShowInventoryCommand();
			}

			return null;
		}
	}
}