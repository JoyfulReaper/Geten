using Geten.Core.Parsers.Script.Syntax;
using Geten.Core.Parsing;
using Geten.Runtime.IO;
using System;

namespace Geten.Runtime
{
	public static class RuntimeMachine
	{
		public static SyntaxNode BuildTreeFromBinaryFile(BinaryGameDefinitionFile file)
		{
			var definitionsBody = file.GetBodyOfSection("definitions");
			var codeBody = file.GetBodyOfSection("code");
			var definitions = BuildDefinitions(definitionsBody);
			var code = BuildCode(codeBody);

			return BlockNode.Concat(definitions, code);
		}

		private static BlockNode BuildCode(byte[] codeBody)
		{
			throw new NotImplementedException();
		}

		private static BlockNode BuildDefinitions(byte[] definitionBody)
		{
			throw new NotImplementedException();
		}
	}
}