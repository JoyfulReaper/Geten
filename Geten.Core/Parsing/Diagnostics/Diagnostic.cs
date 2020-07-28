using Geten.Core.Parsing.Text;

namespace Geten.Core.Parsing.Diagnostics
{
    public sealed class Diagnostic
    {
        public Diagnostic(TextSpan location, string message)
        {
            Location = location;
            Message = message;
        }

        public TextSpan Location { get; }
        public string Message { get; }

        public override string ToString() => Message;
    }
}