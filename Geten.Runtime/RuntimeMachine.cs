using Geten.Core.Parsers.Script.Syntax;
using Geten.Core.Parsing;
using System.IO;

namespace Geten.Runtime
{
    //Binary represantation of a game
    //File Header
    // Magic Number : int
    // SectionCount : byte
    //Section Header
    // Section Name : string
    // Section Length : short

    // Sections:
    // DefinitionsSection -> contains all definitions of GameObjects
    // MethodTableSection -> contains all Method definitions
    // CodeSection -> contains all runnable code
    // ResourcesSection -> contains all Resources like sounds

    public static class RuntimeMachine
    {
        public static SyntaxNode BuildTreeFromBinary(Stream strm)
        {
            return new BlockNode(null);
        }
    }
}