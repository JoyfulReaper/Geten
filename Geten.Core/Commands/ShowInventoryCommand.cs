using ConsoleTables;

namespace Geten.Core.Commands
{
    internal class ShowInventoryCommand : ITextCommand
    {
        public void Invoke()
        {
            var inv = TextEngine.Player.Inventory;
            var table = new ConsoleTable("Item", "Quantity");

            foreach (var item in inv.GetAll())
            {
                table.AddRow(item.Key.Name, item.Value);
            }

            table.Write();
        }
    }
}