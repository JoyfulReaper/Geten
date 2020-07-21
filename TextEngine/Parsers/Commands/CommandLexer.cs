// go north
// 1. wrtie a GoCommand  with Behavior
//2. check if token is goToken
//3. check if next token is a directiontoken, when yes
//4. consume the next token and build the GoCommand
//5. otherwise throw error

using Geten.Core.Parsing;
using Geten.Core.Parsing.Text;
using System.Collections.Generic;

namespace Geten.Parsers.Commands
{
    public class CommandLexer : BaseLexer<CommandKind>
    {
        public CommandLexer(SourceText src) : base(src)
        {
        }

        public override IEnumerable<Token<CommandKind>> GetAllTokens()
        {
            Token<CommandKind> token;
            do
            {
                token = Lex();
                if (token.Kind != CommandKind.WhiteSpace && token.Kind != CommandKind.EOF)
                    yield return token;
            } while (token.Kind != CommandKind.EOF);
        }

        public override Token<CommandKind> Lex()
        {
            _start = _position;
            switch (Current)
            {
                case '\0':
                    _kind = CommandKind.EOF;
                    break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    ReadNumberToken();
                    break;

                case ' ':
                case '\t':
                case '\n':
                case '\r':
                    ReadWhiteSpace();
                    break;

                default:
                    if (char.IsLetter(Current))
                        ReadCommand();
                    else if (char.IsWhiteSpace(Current))
                        ReadWhiteSpace();

                    break;
            }
            var length = _position - _start;
            var text = _text.ToString(_start, length);

            return new Token<CommandKind>(_kind, _start, text, _value);
        }

        private CommandKind GetCommandKind(string text)
        {
            switch (text)
            {
                case "n":
                case "north":
                case "w":
                case "west":
                case "s":
                case "south":
                case "e":
                case "east":
                case "u":
                case "up":
                case "d":
                case "down":
                    _kind = CommandKind.Direction;
                    _value = TextEngine.GetDirectionFromChar(char.ToUpper(text[0]));
                    break;

                default:
                    if (TextEngine.IsCommand(text))
                    {
                        _kind = CommandKind.Command;
                    }
                    else
                    {
                        _kind = CommandKind.Identifier;
                    }
                    break;
            }

            return _kind;
        }

        private void ReadCommand()
        {
            while (char.IsLetter(Current))
                _position++;

            var length = _position - _start;
            var text = _text.ToString(_start, length);

            GetCommandKind(text);
        }

        private void ReadNumberToken()
        {
            while (char.IsDigit(Current))
                _position++;

            var length = _position - _start;
            var text = _text.ToString(_start, length);
            var value = int.Parse(text);

            _value = value;
            _kind = CommandKind.Number;
        }

        private void ReadWhiteSpace()
        {
            while (char.IsWhiteSpace(Current))
                _position++;

            _kind = CommandKind.WhiteSpace;
        }
    }
}