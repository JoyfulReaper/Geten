using TextEngine.CommandParsing;

namespace TextEngine.Commands
{
    public interface ITextCommand
    {
        ITextCommand Parse(CommandParser parser);
        void Invoke();
    }
}