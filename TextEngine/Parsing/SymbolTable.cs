using System;
using System.Collections.Concurrent;
using System.Reflection.Metadata.Ecma335;

namespace TextEngine.Parsing
{
    public static class SymbolTable
    {
        private static ConcurrentDictionary<CaseInsensitiveString, object> _objects = new ConcurrentDictionary<CaseInsensitiveString, object>();

        public static void Add(CaseInsensitiveString name, object instance)
        {
            if (_objects.ContainsKey(name)) throw new Exception($"'{name}' is already declared");

            _objects.TryAdd(name, instance);
        }

        public static bool Contains(CaseInsensitiveString name) => _objects.ContainsKey(name);

        public static T GetInstance<T>(CaseInsensitiveString name)
        {
            if (!_objects.ContainsKey(name)) throw new Exception($"'{name}' is not declared");

            return (T)_objects[name];
        }
    }
}