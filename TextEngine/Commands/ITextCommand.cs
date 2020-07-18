using TextEngine.CommandParsing;

namespace TextEngine.Commands
{
    public interface ITextCommand
    {
        void Invoke();
    }
}