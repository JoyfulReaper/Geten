using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Geten.Runtime.IO
{
    public class BinaryGameSectionHeader
    {
        public string Name { get; set; }
        public int SectionLength { get; set; }
    }
}