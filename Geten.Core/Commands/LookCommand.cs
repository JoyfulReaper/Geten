using System;
using System.Text;
using Geten.Core.GameObjects;
using Geten.TextProcessing;
using Geten.TextProcessing.Interfaces;
using Geten.TextProcessing.Synonyms;

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
			  // I think I need to re-think this need a way to look in Container items also
			  // This is super ugly need to re-think once it all works....

				var room = TextEngine.Player.Location;
				var items = room.Inventory.GetAll();
				foreach (var item in items)
				{
					if (item.Key.Name.ToLower() == lookAt.ToLower()) // Look for the target in the room
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
						}
						return;
					}
					if (item.Key is ContainerItem ci) // Check containers in the room
					{
						foreach (var ciItem in ci.Inventory.GetAll())
						{
							if (ciItem.Key.Name.ToLower() == lookAt.ToLower() && ci.GetProperty<bool>("lookedAt"))
							{
								TextEngine.AddMessage(ciItem.Key.Description);
								return;
							}
						}
					}
				}
			}
			TextEngine.AddMessage($"You look very carefully, but you don't see {lookAt}.");
		}
	}
}