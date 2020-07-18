using System;
using System.Collections.Generic;
using System.IO;
using TextEngine.Parsing.Syntax;

namespace TextEngine.Parsing
{
    public class ScriptParser : BaseParser<SyntaxKind, ScriptLexer, SyntaxNode>
    {

        protected override SyntaxNode InternalParse()
        {
            var value = ParseBlock();
            MatchToken(SyntaxKind.EOF);

            return value;
        }



        public bool MatchCurrentKeyword(string keyword)
        {
            return Current.Kind == SyntaxKind.Keyword && Current.Text == keyword;
        }

        public bool MatchNextKeyword(string keyword)
        {
            return Peek(1).Kind == SyntaxKind.Keyword && Peek(1).Text == keyword;
        }

        public SyntaxNode ParseBlock()
        {
            var members = ParseMembers();

            return new BlockNode(members);
        }

        private IEnumerable<SyntaxNode> ParseMembers()
        {
            var members = new List<SyntaxNode>();

            while (Current.Kind != SyntaxKind.EOF)
            {
                var startToken = Current;
                var member = ParseMember();

                if (member is BlockNode bn)
                {
                    members.AddRange(bn.Children);
                }
                else
                {
                    members.Add(member);
                }

                // If ParseMember() did not consume any tokens,
                // we need to skip the current token and continue
                // in order to avoid an infinite loop.
                //
                // We don't need to report an error, because we'll
                // already tried to parse an expression statement
                // and reported one.
                if (Current == startToken)
                    NextToken();
            }

            return members;
        }

        private SyntaxNode ParseMember()
        {
            //ToDo: refactor ParseMember to a Dictionary to reduce branches
            if(MatchCurrentKeyword("character"))
            {
                return ParsePropertyOnly<CharacterDefinitionNode>("character");
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
            else if(MatchCurrentKeyword("memory"))
            {
                return ParseMemorySlot();
            }
            else if(MatchCurrentKeyword("on"))
            {
                return ParseEventSubscription();
            }
            else if(MatchCurrentKeyword("ask"))
            {
                return ParseAskFor();
            }
            else if (MatchCurrentKeyword("room"))
            {
                return ParsePropertyOnly<RoomDefinitionNode>("room");
            }
            else if(MatchCurrentKeyword("command"))
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

            return null;
        }

        private T ParseTargeted<T>(string first, string second)
            where T : TargetedNode
        {
            Token<SyntaxKind> target = null;

            var action = MatchKeyword(first);
            var argument = MatchKeyword(second);
            var name = MatchToken(SyntaxKind.String);
            Token<SyntaxKind> fromkeyword = null;
            if  (MatchNextKeyword("from"))
            {
                fromkeyword = NextToken(); 
                target = MatchToken(SyntaxKind.String);
            }

            var result = (T)Activator.CreateInstance(typeof(T), action, argument, name, fromkeyword, target);

            return result;
        }

        private SyntaxNode ParseRemove()
        {
            return ParseTargeted<RemoveItemNode>("remove", "item");
        }

        private SyntaxNode ParseAdd()
        {
            return ParseTargeted<AddItemNode>("add", "item");
        }

        private SyntaxNode ParseDecrease()
        {
            throw new NotImplementedException();
        }

        private SyntaxNode ParseSetProperty()
        {
            var setPropertykeyword = MatchKeyword("setProperty");
            Token<SyntaxKind> target = null;
            if (MatchNextKeyword("of"))
            {
                target = MatchToken(SyntaxKind.String);
            }
            var property = MatchToken(SyntaxKind.Keyword);
            var value = ParseLiteral();

            return new SetPropertyNode(setPropertykeyword, target, property, value);
        }

        private SyntaxNode ParseIncrease()
        {
            var increasekeyword = MatchKeyword("increase");
            var target = MatchToken(SyntaxKind.Keyword);
            var ofkeyword = MatchKeyword("of");
            var instance = MatchToken(SyntaxKind.String);
            var bykeyword = MatchKeyword("by");
            var increaseAmount = MatchToken(SyntaxKind.Number);

            return new IncreaseNode(increasekeyword, target, ofkeyword, instance, bykeyword, increaseAmount);
        }

        private SyntaxNode ParseCommand()
        {
            var commandkeyword = MatchKeyword("command");
            var commandString = MatchToken(SyntaxKind.String);

            return new CommandNode((string)commandString.Value);
        }

        private SyntaxNode ParseAskFor()
        {
            var askkeyword = MatchKeyword("ask");
            var forKeyword = MatchKeyword("for");
            var message = MatchToken(SyntaxKind.String);
            var toKeyword = MatchKeyword("to");
            var slotname = MatchToken(SyntaxKind.String);

            return new AskForInputNode(askkeyword, forKeyword, message, toKeyword, slotname);
        }

        private SyntaxNode ParseEventSubscription()
        {
            var keyword = MatchKeyword("on");
            var name = MatchToken(SyntaxKind.String);
            var body = (BlockNode)ParseProcedureBlock();

            return new EventSubscriptionNode(name.Value.ToString(), body);
        }

        private BlockNode ParseProcedureBlock()
        {
            var members = new List<SyntaxNode>();

            while(Current.Kind != SyntaxKind.EndToken && Current.Kind != SyntaxKind.EOF)
            {
                members.Add(ParseMember());
            }

            MatchToken(SyntaxKind.EndToken);

            return new BlockNode(members);
        }

        private SyntaxNode ParseMemorySlot()
        {
            var keyword = MatchKeyword("memory");
            var slotname = MatchToken(SyntaxKind.String);
            object initialvalue = null;
            if (MatchCurrentKeyword("equals"))
            {
                var equalsToken = MatchKeyword("equals");
                initialvalue = MatchToken(SyntaxKind.String).Value; // replace with ParseValue()
            }

            return new MemorySlotDefinition(slotname.Value.ToString(), initialvalue);
        }

        private SyntaxNode ParseTell()
        {
            MatchKeyword("tell");
            var message = MatchToken(SyntaxKind.String);

            return new TellNode(message.Value.ToString());
        }

        private SyntaxNode ParseInclude()
        {
            MatchKeyword("include");
            var filename = MatchToken(SyntaxKind.String);

            var p = new ScriptParser();
            var tree = p.Parse(File.ReadAllText(filename.Value.ToString()));

            return tree;
        }

        private T ParsePropertyOnly<T>(string keyword)
            where T : PropertyOnlyBasedCommand
        {
            MatchKeyword(keyword);
            var name = MatchToken(SyntaxKind.String);
            MatchKeyword("with");
            var properties = ParsePropertyList();
            BlockNode body = new BlockNode(null);

            if (Current.Kind != SyntaxKind.EndToken && Current.Kind != SyntaxKind.EOF) //optional procedure block
            {
                body = ParseProcedureBlock();
            }

            var result = (T)Activator.CreateInstance(typeof(T), name.Text, properties, body);

            return result;
        }

        private PropertyList ParsePropertyList()
        {
            var result = new PropertyList();

            var parseNextArgument = true;
            while (parseNextArgument &&
                   Current.Kind != SyntaxKind.EndToken &&
                   Current.Kind != SyntaxKind.EOF)
            {
                var prop = ParseProperty();
                result.Add(prop.name, prop.value);

                if(!AcceptKeyword("and", out var andToken))
                {
                    parseNextArgument = false;
                }
            }

            MatchToken(SyntaxKind.EndToken);
            return result;
        }



        public bool AcceptKeyword(string keyword, out Token<SyntaxKind> token)
        {
            try
            {
                token = MatchKeyword(keyword);
                return true;
            }
            catch
            {
                token = new Token<SyntaxKind>(SyntaxKind.BadToken, -1, null, null);
                return false;
            }
        }

        private (string name, SyntaxNode value) ParseProperty()
        {
            var name = MatchToken(SyntaxKind.Keyword);
            var value = ParseLiteral();

            return (name.Text, value);
        }

        private SyntaxNode ParseLiteral()
        {
            SyntaxNode result = null;
            switch (Current.Kind)
            {
                case SyntaxKind.String:
                    result = new LiteralNode(Current.Text);
                    break;
                case SyntaxKind.Number:
                    result = new LiteralNode(int.Parse(Current.Text));
                    break;
                case SyntaxKind.Boolean:
                    result = new LiteralNode(bool.Parse(Current.Text));
                    break;
                default:
                    throw new Exception("Only String and Number as Literal accepted");
            }

            NextToken();

            return result;
        }

        public Token<SyntaxKind> MatchKeyword(string keyword)
        {
            var keywordToken = MatchToken(SyntaxKind.Keyword);
            if(keywordToken.Text == keyword)
            {
                return keywordToken;
            }

            throw new Exception($"expected '{keyword}' got '{keywordToken.Text}'");
        }
    }
}