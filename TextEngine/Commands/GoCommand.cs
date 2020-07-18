using TextEngine.CommandParsing;

namespace TextEngine.Commands
{
    [CommandName("go")]
    class GoCommand : ITextCommand
    {
        public GoCommand(Direction direction)
        {
            Direction = direction;
        }
        public GoCommand()
        {

        }

        public Direction Direction { get; }

        public void Invoke()
        {
            //ToDo: move the Player
        }

        public ITextCommand Parse(CommandParser parser)
        {
            var direction = parser.MatchToken(CommandKind.Direction);

            return new GoCommand((Direction)direction.Value);
        }
    }
}
