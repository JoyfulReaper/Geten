using System;
using Geten.TextProcessing.Synonyms;

namespace Geten.TextProcessing
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class CommandHandlerAttribute : Attribute
	{
		public CommandHandlerAttribute(VerbCodes verb)
		{
			Verb = verb;
		}

		public VerbCodes Verb { get; }
	}
}