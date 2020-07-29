using Geten.Core;

namespace Geten.Runtime.IO
{
    public class GameBinaryBuilder : IGameBinaryBuilder
    {
        private BinaryGameDefinitionFile _file = new BinaryGameDefinitionFile();

        public static GameBinaryBuilder Build()
        {
            return new GameBinaryBuilder();
        }

        public BinaryGameDefinitionFile GetFile()
        {
            return _file;
        }
    }
}