using Geten.Core.Parsers.Script.Syntax;
using Geten.Core.Parsing;
using Geten.Runtime.IO;

namespace Geten.Runtime
{
    public static class RuntimeMachine
    {
        public static SyntaxNode BuildTreeFromBinaryFile(BinaryGameDefinitionFile file)
        {
            return new BlockNode(null);
        }
    }
}