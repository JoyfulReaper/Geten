using TextEngine.CommandParsing;
using TextEngine.MapItems;

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
            MapSite location = TextEngine.Player.Location.GetSide(Direction);
            location.Enter(TextEngine.Player, Direction);
        }
    }
}
