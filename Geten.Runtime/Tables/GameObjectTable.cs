using Geten.Core;
using Geten.Core.Parsers.Script.Syntax;
using Geten.Core.Parsing;
using System;
using System.IO;
using System.Reflection;
using Geten.Core.Parsers.Script;

namespace Geten.Runtime.Tables
{
	public class GameObjectTable : BinaryTable<GameObjectKind, PropertyList> //ToDo: need to rethink
	{
		public void Add(GameObject obj)
		{
			var attr = obj.GetType().GetCustomAttribute<GameObjectKindAttribute>();
			if (attr != null)
			{
				//Add(attr.Kind, obj.GetAllProperties().ToPropertyList());
			}

			throw new Exception($"Object '{obj.GetType().Name}' does not have a object kind.");
		}

		/*public T GetObjectsByKind<T>(GameObjectKind)
					where T : GameObject
		{
		}*/

		public override GameObjectKind ReadKey(BinaryReader br)
		{
			return (GameObjectKind)br.ReadByte();
		}

		public override PropertyList ReadValue(BinaryReader br)
		{
			var props = new PropertyList();
			var count = br.ReadInt32();

			for (var i = 0; i < count; i++)
			{
				var key = br.ReadString();
				var type = (TypeCode)br.ReadByte();
				object value = null;

				switch (type)
				{
					case TypeCode.String:
						{
							value = br.ReadString();
							break;
						}

					case TypeCode.Boolean:
						{
							value = br.ReadBoolean();
							break;
						}

					case TypeCode.Int32:
						{
							value = br.ReadInt32();
							break;
						}
				}

				props.Add(key, value);
			}

			return props;
		}

		public override void WriteKey(BinaryWriter bw, GameObjectKind key)
		{
			bw.Write((byte)key);
		}

		public override void WriteValue(BinaryWriter bw, PropertyList props)
		{
			bw.Write(props.Count);

			foreach (var prop in props)
			{
				bw.Write(prop.Key);

				var typeCode = Type.GetTypeCode(prop.Value.GetType());
				bw.Write((byte)typeCode);

				switch (typeCode)
				{
					case TypeCode.String:
						{
							bw.Write((string)props[prop.Key]);
							break;
						}

					case TypeCode.Boolean:
						{
							bw.Write((bool)props[prop.Key]);
							break;
						}

					case TypeCode.Int32:
						{
							bw.Write((int)props[prop.Key]);
							break;
						}
				}
			}
		}
	}
}