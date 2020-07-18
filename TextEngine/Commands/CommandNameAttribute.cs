using System;
using TextEngine.CommandParsing;

namespace TextEngine.Commands
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CommandNameAttribute : Attribute
    {
        public CommandNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}