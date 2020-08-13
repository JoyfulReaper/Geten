using System.Text;
using Geten.Core;
using Geten.Runtime.Binary;
using Geten.Runtime.IO;

namespace Geten.Runtime.IO
{
	public static class TableExtensions
	{
		public static IGameBinaryBuilder AddStringSection(this IGameBinaryBuilder builder, CaseSensisitiveString name, string table)
		{
			builder.AddSection(name, Encoding.ASCII.GetBytes(table));

			return builder;
		}

		public static IGameBinaryBuilder AddTableSection<T>(this IGameBinaryBuilder builder, CaseSensisitiveString name, T table)
					where T : IBinaryTable, new()
		{
			builder.AddSection(name, table.Save());

			return builder;
		}

		public static IGameBinaryBuilder AddTableSection<T>(this IGameBinaryBuilder builder, T table)
			where T : IBinaryTable, new()
		{
			builder.AddSection(typeof(T).Name, table.Save());

			return builder;
		}

		public static T GetTable<T>(this BinaryGameSection s)
					where T : IBinaryTable, new()
		{
			var instance = new T();
			instance.Load(s.Body);

			return instance;
		}

		public static T GetTable<T>(this BinaryGameDefinitionFile f, CaseSensisitiveString name)
		   where T : IBinaryTable, new()
		{
			return f[name].GetTable<T>();
		}

		public static T GetTable<T>(this BinaryGameDefinitionFile f)
		  where T : IBinaryTable, new()
		{
			return f[typeof(T).Name].GetTable<T>();
		}
	}
}