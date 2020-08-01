using Geten.Core;
using Polenter.Serialization;
using System.IO;

namespace Geten.Runtime.Tables
{
	public class GameObjectTable : BinaryTable<CaseInsensitiveString, GameObject> //ToDo: need to rethink
	{
		private SharpSerializer _serializer;

		public GameObjectTable()
		{
			_serializer = new SharpSerializer(true);
		}

		public override CaseInsensitiveString ReadKey(BinaryReader br)
		{
			return br.ReadString();
		}

		public override GameObject ReadValue(BinaryReader br)
		{
			var byteCount = br.ReadInt32();
			var raw = br.ReadBytes(byteCount);

			return (GameObject)_serializer.Deserialize(new MemoryStream(raw));
		}

		public override void WriteKey(BinaryWriter bw, CaseInsensitiveString key)
		{
			bw.Write(key);
		}

		public override void WriteValue(BinaryWriter bw, GameObject obj)
		{
			var ms = new MemoryStream();
			_serializer.Serialize(obj, ms);
			var raw = ms.ToArray();

			bw.Write(raw.Length);
			bw.Write(raw);
			//ToDo: serialize GameObject
		}
	}
}