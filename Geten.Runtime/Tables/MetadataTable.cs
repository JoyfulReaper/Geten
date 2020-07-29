using Geten.Core;
using System.IO;

namespace Geten.Runtime.Tables
{
    public class MetadataTable : BinaryTable<CaseInsensitiveString, CaseInsensitiveString>
    {
        public override CaseInsensitiveString ReadKey(BinaryReader br) => br.ReadString();

        public override CaseInsensitiveString ReadValue(BinaryReader br) => br.ReadString();

        public override void WriteKey(BinaryWriter bw, CaseInsensitiveString key)
        {
            bw.Write(key);
        }

        public override void WriteValue(BinaryWriter bw, CaseInsensitiveString value)
        {
            bw.Write(value);
        }
    }
}