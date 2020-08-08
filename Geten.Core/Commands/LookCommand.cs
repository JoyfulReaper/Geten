using System.Text;
using System.Xml;
using Geten.Core.GameObjects;
using Geten.Core.MapItems;

namespace Geten.Core.Commands
{
	internal class LookCommand : ITextCommand
	{
		public LookCommand(string lookAt)
		{
			LookAt = lookAt;
		}

		private string LookAt { get; }

		public void Invoke()
		{
			if (LookAt == null)
			{
				dynamic loc = TextEngine.Player?.Location;
				loc.lookedAt = true;
				TextEngine.AddMessage(loc.LookDescription);
				return;
			}
			else
			{ // Check room, then check containers in room, then check player, then check containers in player...
			  // Then check NPCs in the room. Anything else?
			  // I think I need to re-think this need a way to look in Container items also
			  // This is super ugly need to re-think once it all works....
				var room = TextEngine.Player.Location;
				var items = room.Inventory.GetAll();
				foreach (var i in items)
				{
					if (i.Key.Name.ToLower() == LookAt.ToLower()) // check the room
					{
						TextEngine.AddMessage(i.Key.Description);
						if (SymbolTable.GetInstance<ContainerItem>(LookAt) is ContainerItem container)
						{
							container.SetProperty("lookedAt", true);
							TextEngine.AddMessage($"The {container.Name} contains: ");
							var sb = new StringBuilder();
							foreach (var itemInContainer in container.Inventory.GetAll())
							{
								sb.Append($"({itemInContainer.Value}) {itemInContainer.Key.Name}\n");
							}
							TextEngine.AddMessage(sb.ToString());
						}
						return;
					}
					if (i.Key is ContainerItem ci) // Check containers in the room
					{
						foreach (var ciItem in ci.Inventory.GetAll())
						{
							if (ciItem.Key.Name.ToLower() == LookAt.ToLower() && ci.GetProperty<bool>("lookedAt"))
							{
								TextEngine.AddMessage(ciItem.Key.Description);
								return;
							}
						}
					}
				}
			}
			TextEngine.AddMessage($"You look very carefully, but you don't see {LookAt}.");
		}
	}
}