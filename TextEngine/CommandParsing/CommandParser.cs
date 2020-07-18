using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TextEngine.Commands;
using TextEngine.Parsing;

namespace TextEngine.CommandParsing
{
    public class CommandParser : BaseParser<CommandKind, CommandLexer, ITextCommand>
    {
        internal static Dictionary<string, Func<ITextCommand>> _commandParsers = new Dictionary<string, Func<ITextCommand>>();

        public CommandParser()
        {
            if (_commandParsers.Count == 0)
            {
                var types = Assembly.GetExecutingAssembly().GetTypes();
                var commandTypes = types.Where(_ => typeof(ITextCommand).IsAssignableFrom(_));

                foreach (var t in commandTypes)
                {
                    var attr = t.GetCustomAttributes<CommandNameAttribute>();
                    if (attr.Any())
                    {
                        foreach (var a in attr)
                        {
                            var instance = (ITextCommand)Activator.CreateInstance(t);

                            _commandParsers.Add(a.Name, () => instance.Parse(this));
                        }
                    }
                }
            }
        }

        protected override ITextCommand InternalParse()
        {
            if(Current.Kind == CommandKind.Command)
            {
                if(_commandParsers.ContainsKey(Current.Text))
                {
                    var handler = _commandParsers[Current.Text];
                    NextToken(); // consume token before invoke handler
                    return handler();
                }
                else
                {
                    throw new Exception($"Command '{Current.Kind}' not found");
                }
            }
            else
            {
                throw new Exception($"'{Current.Kind}' is not a valid Command");
            }
        }
    }
}