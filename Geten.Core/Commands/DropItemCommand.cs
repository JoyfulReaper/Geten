using Geten.Core.MapItems;
using Geten.Core.Repositorys;

namespace Geten.Core.Commands
{
	internal class DropItemCommand : ITextCommand
	{
		public DropItemCommand(string itemname)
		{
			ItemName = itemname;
		}

		public string ItemName { get; }

		public void Invoke()
		{
			TextEngine.Player.Inventory.DropItem(ItemName);
		}
	}
}