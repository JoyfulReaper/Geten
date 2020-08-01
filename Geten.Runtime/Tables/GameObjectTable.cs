using Geten.Core;
using System.IO;

namespace Geten.Runtime.Tables
{
	public class GameObjectTable : BinaryTable<string, GameObject> //ToDo: need to rethink
	{
		public override string ReadKey(BinaryReader br)
		{
			return br.ReadString();
		}

		public override GameObject ReadValue(BinaryReader br)
		{
			//ToDo: deserialize GameObject

			return null;
		}

		public override void WriteKey(BinaryWriter bw, string key)
		{
			bw.Write(key);
		}

		public override void WriteValue(BinaryWriter bw, GameObject props)
		{
			//ToDo: serialize GameObject
		}
	}
}