using Geten.Core;
using Geten.GameObjects;

namespace Geten.Commands
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