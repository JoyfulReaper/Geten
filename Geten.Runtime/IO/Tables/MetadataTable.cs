using Geten.Core;
using Geten.Runtime.IO;
using System.IO;

namespace Geten.Runtime.IO.Tables
{
	public class MetadataTable : BinaryTable<CaseSensisitiveString, CaseSensisitiveString>
	{
		public override CaseSensisitiveString ReadKey(BinaryReader br) => br.ReadString();

		public override CaseSensisitiveString ReadValue(BinaryReader br) => br.ReadString();

		public override void WriteKey(BinaryWriter bw, CaseSensisitiveString key)
		{
			bw.Write(key);
		}

		public override void WriteValue(BinaryWriter bw, CaseSensisitiveString value)
		{
			bw.Write(value);
		}
	}
}