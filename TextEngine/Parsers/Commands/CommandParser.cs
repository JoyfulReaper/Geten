using Geten.Commands;
using Geten.Core.Parsing;
using System;

//N"orth, "S"outh, "E"ast, "W"est, Up, Down, look, go, self, get, inv, drop, say, use (as well as synonyms.)

namespace Geten.Parsers.Commands
{
    public class CommandParser : BaseParser<CommandKind, CommandLexer, ITextCommand>
    {
        protected override ITextCommand InternalParse()
        {
            if (Current.Kind == CommandKind.Direction)
            {
                ParseGoCommand();
            }
            if (Current.Kind == CommandKind.Command)
            {
                NextToken();

                switch (Peek(-1).Text)
                {
                    case "go":
                        return ParseGoCommand();

                    case "look":
                        return ParseLookAt();

                    case "pickup":
                        return ParsePickup();

                    case "quit":
                    case "exit":
                        return ParseQuit();

                    case "inv":
                        return new ShowInventoryCommand();

                    case "show":
                        return ParseShowCommand();

                    default:
                        return null;
                }
            }
            else
            {
                throw new Exception($"'{Current.Kind}' is not a valid Command");
            }
        }

        private ITextCommand ParseGoCommand()
        {
            Direction dir = Direction.Invalid;
            if (Peek(-1).Kind == CommandKind.Direction)
            {
                dir = (Direction)Peek(-1).Value;
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