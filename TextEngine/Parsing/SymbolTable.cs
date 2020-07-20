using System;
using System.Collections.Generic;

namespace TextEngine.Parsing
{
    public static class SymbolTable
    {
        private static Dictionary<string, object> _objects = new Dictionary<string, object>();

        public static void Add(string name, object instance)
        {
            if (_objects.ContainsKey(name)) throw new Exception($"'{name}' is already declared");

            _objects.Add(name, instance);
        }

        public static T GetInstance<T>(string name)
        {
            if (!_objects.ContainsKey(name)) throw new Exception($"'{name}' is not declared");

            return (T)_objects[name];
        }
    }
}