using System.Collections.Generic;
using System.IO;

namespace Geten.Runtime.IO
{
    public class BinaryGameFileHeader
    {
        public int MagicNumber { get; set; }
        public byte SectionCount { get; set; }
    }
}