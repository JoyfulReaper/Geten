using System;
using System.Collections;
using System.Collections.Generic;
using TextEngine.Parsing.Text;

namespace TextEngine.Parsing.Diagnostics
{
    public sealed class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();

        public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();

        public void AddRange(DiagnosticBag diagnostics)
        {
            _diagnostics.AddRange(diagnostics._diagnostics);
        }

        private void Report(TextSpan span, string message)
        {
            var diagnostic = new Diagnostic(span, message);
            _diagnostics.Add(diagnostic);
        }

        public void ReportBadCharacter(TextSpan location, char character)
        {
            var message = $"Bad character input: '{character}'.";
            Report(location, message);
        }

        public void ReportUnexpectedToken<KindType>(TextSpan span, KindType actualKind, KindType expectedKind)
        {
            var message = $"Unexpected token <{actualKind}>, expected <{expectedKind}>.";
            Report(span, message);
        }

        public void ReportInvalidNumber(TextSpan span, string text)
        {
            var message = $"The number {text} isn't valid.";
            Report(span, message);
        }

        public void ReportUnterminatedString(TextSpan span)
        {
            var message = "Unterminated string literal.";
            Report(span, message);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _diagnostics.GetEnumerator();
        }

        internal void ReportUnexpectedKeyword<KindType>(TextSpan location, Token<KindType> keywordToken, string keyword)
        {
            var message = $"Expected '{keyword}' got '{keywordToken.Text}'.";
            Report(location, message);
        }

        internal void ReportUnexpectedKeyword<KindType>(TextSpan location, Token<KindType> keywordToken)
        {
            var message = $"Unexpected '{keywordToken.Text}'.";
            Report(location, message);
        }

        internal void ReportUnexpectedLiteral<KindType>(TextSpan location, KindType kind)
        {
            var message = $"Unexpected Literal '{kind}'.";
            Report(location, message);
        }
    }
}