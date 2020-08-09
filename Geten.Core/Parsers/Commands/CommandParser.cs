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
			if (Current.Kind == CommandKind.Direction)
			{
				return ParseGoCommand();
			}
			ITextCommand command;
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
				Diagnostics.ReportInvalidCommand(Current.Text);
				return null;
			}

			MatchToken(CommandKind.EOF);
			return command;
		}

		private ITextCommand ParseDrop()
		{
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

			return new GoCommand();
		}

		private ITextCommand ParseLookAt()
		{
			MatchToken(CommandKind.Command);
			if (Current.Kind != CommandKind.Identifier)
			{
				//return new LookCommand(null);
				return new LookCommand();
			}
			else
			{
				if (Current.Text == "at" || Current.Text == "in")
				{
					MatchToken(CommandKind.Identifier);
					if (Current.Text == "the")
					{
						MatchToken(CommandKind.Identifier);
					}
				}
				var sb = new StringBuilder(); // This should be a method, probably want it a lot
				do
				{
					var token = MatchToken(CommandKind.Identifier);
					sb.Append(token.Text).Append(' ');
				} while (Current.Kind == CommandKind.Identifier && Current.Kind != CommandKind.EOF);
				//return new LookCommand(sb.ToString().Trim());
				return new LookCommand();
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