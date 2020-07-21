using Geten.MapItems;

namespace Geten.Commands
{
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
