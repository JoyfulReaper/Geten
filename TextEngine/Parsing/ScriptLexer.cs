﻿using System;
using System.Collections.Generic;
using System.Text;
using TextEngine.Parsing.Text;

namespace TextEngine.Parsing
{
    /// <summary>
    ///  A Tokenizer
    /// </summary>
    public sealed class ScriptLexer : BaseLexer<SyntaxKind>
    {
        public ScriptLexer(SourceText src) : base (src)
        {
        }

        public override IEnumerable<Token<SyntaxKind>> GetAllTokens()
        {
            Token<SyntaxKind> token;
            do
            {
                token = Lex();

                if (token.Kind != SyntaxKind.Whitespace &&
                    token.Kind != SyntaxKind.BadToken)
                {
                    yield return token;
                }
            } while (token.Kind != SyntaxKind.EOF);
        }

        public override Token<SyntaxKind> Lex()
        {
            _start = _position;
            _kind = SyntaxKind.BadToken;
            _value = null;

            switch (Current)
            {
                case '\0':
                    _kind = SyntaxKind.EOF;
                    break;
                case '"':
                    ReadDoubleQuotedString();
                    break;
                case '\'':
                    ReadSingleQuotedString();
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
                    {
                        ReadKeyword();
                    }
                    else if (char.IsWhiteSpace(Current))
                    {
                        ReadWhiteSpace();
                    }
                    else
                    {
                        var span = new TextSpan(_position, 1);
                        _diagnostics.ReportBadCharacter(span, Current);
                        _position++;
                    }

                    break;
            }

            var length = _position - _start;
            var text = _text.ToString(_start, length);

            return new Token<SyntaxKind>(_kind, _start, text, _value);
        }

        private void ReadKeyword()
        {
            while (char.IsLetter(Current))
                _position++;

            var length = _position - _start;
            var text = _text.ToString(_start, length);

            if (text == "end")
            {
                _kind = SyntaxKind.EndToken;
                return;
            }
            else if (text == "true" || text == "false")
            {
                _kind = SyntaxKind.Boolean;
                _value = Boolean.Parse(text);
                return;
            }
            else
            {
                _kind = SyntaxKind.Keyword;
            }
        }

        private void ReadSingleQuotedString()
        {
            // Skip the current quote
            _position++;

            var sb = new StringBuilder();
            var done = false;

            while (!done)
            {
                switch (Current)
                {
                    case '\0':
                    case '\r':
                    case '\n':
                        var span = new TextSpan(_start, 1);
                        _diagnostics.ReportUnterminatedString(span);
                        done = true;
                        break;
                    case '\'':
                        if (Lookahead == '\'')
                        {
                            sb.Append(Current);
                            _position += 2;
                        }
                        else
                        {
                            _position++;
                            done = true;
                        }
                        break;
                    default:
                        sb.Append(Current);
                        _position++;
                        break;
                }
            }

            _kind = SyntaxKind.String;
            _value = sb.ToString();
        }

        private void ReadDoubleQuotedString()
        {
            // Skip the current quote
            _position++;

            var sb = new StringBuilder();
            var done = false;

            while (!done)
            {
                switch (Current)
                {
                    case '\0':
                    case '\r':
                    case '\n':
                        var span = new TextSpan(_start, 1);
                        _diagnostics.ReportUnterminatedString(span);
                        done = true;
                        break;
                    case '"':
                        if (Lookahead == '"')
                        {
                            sb.Append(Current);
                            _position += 2;
                        }
                        else
                        {
                            _position++;
                            done = true;
                        }
                        break;
                    default:
                        sb.Append(Current);
                        _position++;
                        break;
                }
            }

            _kind = SyntaxKind.String;
            _value = sb.ToString();
        }

        private void ReadWhiteSpace()
        {
            while (char.IsWhiteSpace(Current))
                _position++;

            _kind = SyntaxKind.Whitespace;
        }

        private void ReadNumberToken()
        {
            while (char.IsDigit(Current))
                _position++;

            var length = _position - _start;
            var text = _text.ToString(_start, length);
            if (!int.TryParse(text, out var value))
            {
                var span = new TextSpan(_start, length);
                _diagnostics.ReportInvalidNumber(span, text);
            }

            _value = value;
            _kind = SyntaxKind.Number;
        }
    }
}