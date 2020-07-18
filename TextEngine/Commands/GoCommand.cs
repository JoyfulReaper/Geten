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
    }
}
