using Geten.Core;

namespace Geten.Runtime.IO
{
    public class BinaryGameSectionHeader
    {
        public CaseInsensitiveString Name { get; set; }
        public int SectionLength { get; set; }
    }
}