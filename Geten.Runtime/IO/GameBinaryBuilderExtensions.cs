using Geten.Core;

namespace Geten.Runtime.IO
{
    public static class GameBinaryBuilderExtensions
    {
        public static IGameBinaryBuilder AddSection(this IGameBinaryBuilder builder, CaseInsensitiveString name, byte[] body)
        {
            var s = new BinaryGameSection();
            s.Header.Name = name;
            s.Header.SectionLength = body.Length;
            s.Body = body;

            builder.GetFile().Header.SectionCount++;
            builder.GetFile().Sections.Add(s);

            return builder;
        }
    }
}