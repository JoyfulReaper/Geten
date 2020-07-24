namespace Geten.Commands
{
    internal class ShowInventoryCommand : ITextCommand
    {
        public void Invoke()
        {
            var inv = TextEngine.Player.Inventory;
            //ToDo: show inventory in a table
        }
    }
}