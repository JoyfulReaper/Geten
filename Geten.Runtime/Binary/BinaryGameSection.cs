namespace Geten.Runtime.Binary
{
	public class BinaryGameSection
	{
		public byte[] Body { get; set; }
		public BinaryGameSectionHeader Header { get; set; } = new BinaryGameSectionHeader();
	}
}