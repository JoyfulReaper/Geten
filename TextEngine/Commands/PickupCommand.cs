using TextEngine.CommandParsing;

namespace TextEngine.Commands
{

    [CommandName("pickup")]
    [CommandName("take")]
    class PickupCommand : ITextCommand
    {
        public void Invoke()
        {
            TextEngine.AddMessage("You have pickup an axe");
            TextEngine.Player.Inventory.AddItem(new Item("axe"), 3);
        }
    }
}
