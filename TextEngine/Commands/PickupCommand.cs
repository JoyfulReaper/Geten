using Geten.GameObjects;

namespace Geten.Commands
{
    internal class PickupCommand : ITextCommand
    {
        public void Invoke()
        {
            TextEngine.AddMessage("You have pickup an axe");
            TextEngine.Player.Inventory.AddItem(new Item("axe"), 3);
        }
    }
}