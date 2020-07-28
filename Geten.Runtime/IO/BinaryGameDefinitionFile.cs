using System.Collections.Generic;
using System.IO;

namespace Geten.Runtime.IO
{
    public class BinaryGameDefinitionFile
    {
        public BinaryGameFileHeader Header { get; set; }
        public List<BinaryGameSection> Sections { get; set; } = new List<BinaryGameSection>();

        public static BinaryGameDefinitionFile Load(Stream strm)
        {
            return null;
        }

        public byte[] GetBodyOfSection(string name)
        {
            foreach (var s in Sections)
            {
                if (s.Header.Name == name)
                {
                    return s.Body;
                }
            }

            throw new System.Exception("No Section called '{name}' found");
        }
    }
}