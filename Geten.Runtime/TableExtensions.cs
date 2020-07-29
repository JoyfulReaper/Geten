using Geten.Core;
using Geten.Runtime.IO;

namespace Geten.Runtime
{
    public static class TableExtensions
    {
        public static IGameBinaryBuilder AddTableSection<T>(this IGameBinaryBuilder builder, CaseInsensitiveString name, T table)
            where T : IBinaryTable, new()
        {
            builder.AddSection(name, table.Save());

            return builder;
        }

        public static T GetTable<T>(this BinaryGameSection s)
                    where T : IBinaryTable, new()
        {
            var instance = new T();
            instance.Load(s.Body);

            return instance;
        }

        public static T GetTable<T>(this BinaryGameDefinitionFile f, CaseInsensitiveString name)
           where T : IBinaryTable, new()
        {
            return f[name].GetTable<T>();
        }
    }
}