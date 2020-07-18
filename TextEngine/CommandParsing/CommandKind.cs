namespace TextEngine.CommandParsing
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
