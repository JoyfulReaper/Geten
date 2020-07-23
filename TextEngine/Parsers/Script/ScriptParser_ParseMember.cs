using Geten.Core.Parsing;
using Geten.Parsers.Script.Syntax;

namespace Geten.Parsers.Script
{
    public partial class ScriptParser
    {
        private SyntaxNode ParseMember()
        {
            //ToDo: refactor ParseMember to a Dictionary to reduce branches
            if (MatchCurrentKeyword("character"))
            {
                //return ParsePropertyOnly<CharacterDefinitionNode>("character");
                return ParseCharacter();
            }
            else if (MatchCurrentKeyword("weapon"))
            {
                return ParsePropertyOnly<WeaponDefinitionNode>("weapon");
            }
            else if (MatchCurrentKeyword("key"))
            {
                return ParsePropertyOnly<KeyDefinitionNode>("key");
            }
            else if (MatchCurrentKeyword("include"))
            {
                return ParseInclude();
            }
            else if (MatchCurrentKeyword("tell"))
            {
                return ParseTell();
            }
            else if (MatchCurrentKeyword("memory"))
            {
                return ParseMemorySlot();
            }
            else if (MatchCurrentKeyword("on"))
            {
                return ParseEventSubscription();
            }
            else if (MatchCurrentKeyword("ask"))
            {
                return ParseAskFor();
            }
            else if (MatchCurrentKeyword("room"))
            {
                return ParsePropertyOnly<RoomDefinitionNode>("room");
            }
            else if (MatchCurrentKeyword("exit"))
            {
                return ParsePropertyOnly<ExitDefinitionNode>("exit");
            }
            else if (MatchCurrentKeyword("item"))
            {
                return ParsePropertyOnly<ItemDefinitionNode>("item");
            }
            else if (MatchCurrentKeyword("command"))
            {
                return ParseCommand();
            }
            else if (MatchCurrentKeyword("increase"))
            {
                return ParseIncrease();
            }
            else if (MatchCurrentKeyword("decrease"))
            {
                return ParseDecrease();
            }
            else if (MatchCurrentKeyword("setProperty"))
            {
                return ParseSetProperty();
            }
            else if (MatchCurrentKeyword("add"))
            {
                return ParseAdd();
            }
            else if (MatchCurrentKeyword("remove"))
            {
                return ParseRemove();
            }
            else if (MatchCurrentKeyword("dialog"))
            {
                return ParseDialog();
            }
            else if (MatchCurrentKeyword("play"))
            {
                return ParsePlay();
            }
            else if (MatchCurrentKeyword("recipebook"))
            {
                return ParseRecipeBook();
            }
            else
            {
                Diagnostics.ReportUnexpectedKeyword(Current.Span, Current);
            }

            return null;
        }
    }
}