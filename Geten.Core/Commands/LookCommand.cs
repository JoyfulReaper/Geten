using System;
using System.Text;
using Geten.Core.GameObjects;
using Geten.TextProcessing;
using Geten.TextProcessing.Interfaces;
using Geten.TextProcessing.Synonyms;
using System.Linq;

namespace Geten.Core.Commands
{
	[CommandHandler(VerbCodes.Look)]
	internal class LookCommand : ICommandHandler, ITextCommand
	{
		public LookCommand()
		{
		}

		public void Invoke()
		{
			throw new NotImplementedException();
		}

		public void Invoke(Command cmd)
		{
			var lookAt = cmd.Noun;
			if (string.IsNullOrEmpty(lookAt))
			{
				// Look around the room
				dynamic loc = TextEngine.Player?.Location;
				loc.lookedAt = true;
				TextEngine.AddMessage(loc.Description);
				TextEngine.AddMessage(loc.LookDescription);

				return;
			}
			else
			{ // Check room, then check containers in room, then check player, then check containers in player...
			  // Then check NPCs in the room. Anything else?

				// Check all items in the room and containers in the room
				if (SearchInventory(TextEngine.Player.Location.Inventory, lookAt))
				{
					return;
				}

				// Check in the players inventory
				if (SearchInventory(TextEngine.Player.Inventory, lookAt))
				{
					return;
				}

				// Check for NPCs in the same room
				var npcs = SymbolTable.GetAll<NPC>();
				foreach (var npcInstance in from npcInstance in npcs
											where npcInstance.Name == lookAt
											select npcInstance)
				{
					TextEngine.AddMessage(npcInstance.Description);
					return;
				}
			}
			TextEngine.AddMessage($"You look very carefully, but you don't see {lookAt}.");
		}

		private bool SearchContainerItem(ContainerItem ci, string target)
		{
			foreach (var ciItem in ci.Inventory.GetAll())
			{
				if (ciItem.Key.Name == target && ci.GetProperty<bool>("lookedAt"))
				{
					TextEngine.AddMessage(ciItem.Key.Description);
					return true;
				}
				if (ciItem.Key is ContainerItem c)
				{
					return SearchContainerItem(c, target);
				}
			}

			return false;
		}

		private bool SearchInventory(Inventory inv, string target)
		{
			var items = inv.GetAll();
			foreach (var item in items)
			{
				if (item.Key.Name == target) // Check inventory
				{
					item.Key.SetProperty("lookedAt", true);
					TextEngine.AddMessage(item.Key.Description);
					if (item.Key is ContainerItem container)
					{
						TextEngine.AddMessage($"The {container.Name} contains: ");
						var sb = new StringBuilder();
						foreach (var itemInContainer in container.Inventory.GetAll())
						{
							sb.Append($"({itemInContainer.Value}) {itemInContainer.Key.Name}\n");
						}

						TextEngine.AddMessage(sb.ToString());
						return true;
					}
				}

				if (item.Key is ContainerItem ci)
				{
					return SearchContainerItem(ci, target);
				}
			}
			return false; // Target not found
		}
	}
}