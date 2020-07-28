namespace Geten.Core.Parsers.Commands
{
    public enum CommandKind
    {
        Command,
        Direction,
        Identifier,
        EOF,
        BadToken,
        WhiteSpace,
        Number
    }
}