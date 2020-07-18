using System;
using TextEngine.Commands;
using TextEngine.Parsing;

//N"orth, "S"outh, "E"ast, "W"est, Up, Down, look, go, self, get, inv, drop, say, use (as well as synonyms.) 

namespace TextEngine.CommandParsing
{
    public class CommandParser : BaseParser<CommandKind, CommandLexer, ITextCommand>
    {
        protected override ITextCommand InternalParse()
        {
            if(Current.Kind == CommandKind.Command)
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
                    default:
                        return null;
                }
            }
            else
            {
                throw new Exception($"'{Current.Kind}' is not a valid Command");
            }
        }

        private ITextCommand ParseQuit()
        {
           return new QuitCommand();
        }

        private ITextCommand ParsePickup()
        {
            return new PickupCommand();
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

        private ITextCommand ParseGoCommand()
        {
            var direction = MatchToken(CommandKind.Direction);

            return new GoCommand((Direction)direction.Value);
        }
    }
}