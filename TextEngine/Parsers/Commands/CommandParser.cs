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
                        return ParseQuit();

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
            var direction = MatchToken(CommandKind.Direction);

            return new GoCommand((Direction)direction.Value);
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
            return new ShowInventoryCommand();
        }
    }
}