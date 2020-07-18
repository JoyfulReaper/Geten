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
            //ToDo: parse the go command
            throw new System.NotImplementedException();
        }
    }
}
