using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Geten.TextProcessing.Synonyms;

namespace Geten.TextProcessing
{
	public static class CommandHandler
	{
		private static readonly Dictionary<VerbCodes, ICommandHandler> handlers = new Dictionary<VerbCodes, ICommandHandler>();

		public static void Collect()
		{
			var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(_ => _.GetTypes());
			foreach (var t in types)
			{
				if (typeof(ICommandHandler).IsAssignableFrom(t) && !t.IsInterface)
				{
					var attr = t.GetCustomAttribute<CommandHandlerAttribute>();
					var instance = (ICommandHandler)Activator.CreateInstance(t);

					handlers.Add(attr.Verb, instance);
				}
			}
		}

		public static void Invoke(string command)
		{
			var parser = new Parser();
			var cmd = (Command)parser.ParseCommand(command);

			if (handlers.ContainsKey(cmd.Verb))
			{
				handlers[cmd.Verb].Invoke(cmd);
			}
			else
			{
				throw new Exception($"No Handler for Verb '{cmd.Verb}' found");
			}
		}
	}
}