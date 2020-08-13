using Geten.Core;

namespace Geten.Runtime.Binary
{
	public class GameBinaryBuilder : IGameBinaryBuilder
	{
		private readonly BinaryGameDefinitionFile _file = new BinaryGameDefinitionFile();

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