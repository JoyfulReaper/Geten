using Geten.Core;

namespace Geten.Runtime.IO
{
    public class GameBinaryBuilder
    {
        private BinaryGameDefinitionFile _file = new BinaryGameDefinitionFile();

        public static GameBinaryBuilder Build()
        {
            return new GameBinaryBuilder();
        }

        public static implicit operator BinaryGameDefinitionFile(GameBinaryBuilder builder)
        {
            return builder._file;
        }

        public GameBinaryBuilder AddSection(CaseInsensitiveString name, byte[] body)
        {
            var s = new BinaryGameSection();
            s.Header.Name = name;
            s.Header.SectionLength = body.Length;
            s.Body = body;

            _file.Header.SectionCount++;
            _file.Sections.Add(s);

            return this;
        }
    }
}