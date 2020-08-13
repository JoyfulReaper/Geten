using Geten.Core;

namespace Geten.Runtime.Binary
{
	public class BinaryGameSectionHeader
	{
		public CaseSensisitiveString Name { get; set; }
		public int SectionLength { get; set; }
	}
}