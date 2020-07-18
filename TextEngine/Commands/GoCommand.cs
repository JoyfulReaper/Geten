using TextEngine.CommandParsing;

namespace TextEngine.Commands
{
    class GoCommand : ITextCommand
    {
        public GoCommand(Direction direction)
        {
            Direction = direction;
        }

        public Direction Direction { get; }

        public void Invoke()
        {
            //ToDo: move the Player
        }

        public ITextCommand Parse(CommandParser parser)
        {
            //ToDo: parse the go command
            throw new System.NotImplementedException();
        }
    }
}
