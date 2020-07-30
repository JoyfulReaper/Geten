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
			}
			else
			{ // Check room, then check containers in room, then check player, then check containers in player...
			  // Then check NPCs in the room. Anything else?
				var room = TextEngine.Player.Location;
				var items = room.Inventory.GetAll();
				foreach (var i in items)
				{
					if (i.Key.Name == LookAt) // check the room
					{
						TextEngine.AddMessage(i.Key.Description);
					}
					// TODO check everything else
				}
			}
		}
	}
}