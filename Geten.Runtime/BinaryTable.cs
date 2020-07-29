using System.Collections.Generic;
using System.IO;

namespace Geten.Runtime
{
	public abstract class BinaryTable<TKey, TValue> : Dictionary<TKey, TValue>, IBinaryTable
	{
		public void Load(byte[] raw)
		{
			var ms = new MemoryStream(raw);
			var br = new BinaryReader(ms);

			var count = br.ReadInt32();
			for (var i = 0; i < count; i++)
			{
				Add(ReadKey(br), ReadValue(br));
			}

			br.Close();
		}

		public abstract TKey ReadKey(BinaryReader br);

		public abstract TValue ReadValue(BinaryReader br);

		public byte[] Save()
		{
			var ms = new MemoryStream();
			var bw = new BinaryWriter(ms);

			bw.Write(Count);
			foreach (var item in this)
			{
				WriteKey(bw, item.Key);
				WriteValue(bw, item.Value);
			}

			bw.Close();

			return ms.ToArray();
		}

		public abstract void WriteKey(BinaryWriter bw, TKey key);

		public abstract void WriteValue(BinaryWriter bw, TValue value);
	}
}