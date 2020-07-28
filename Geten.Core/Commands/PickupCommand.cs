using Geten.Core.GameObjects;

namespace Geten.Core.Commands
{
    internal class PickupCommand : ITextCommand
    {
        public void Invoke()
        {
            TextEngine.AddMessage("You have pickup an axe");
            TextEngine.Player.Inventory.AddItem(GameObject.Create<Item>("axe"), 3);
        }
    }
}