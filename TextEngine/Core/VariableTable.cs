using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Geten.Core
{
    // Almost the same as SymbolTable should inherit from same baseclass.
    public static class VariableTable
    {
        private static readonly ConcurrentDictionary<CaseInsensitiveString, object> _variables = new ConcurrentDictionary<CaseInsensitiveString, object>();

        public static void Add(CaseInsensitiveString name, object instance)
        {
            if (_variables.ContainsKey(name)) throw new Exception($"'{name}' is already declared");

            _variables.TryAdd(name, instance);
        }

        public static void ClearAllSymbols() => _variables.Clear();

        public static bool Contains(CaseInsensitiveString name) => _variables.ContainsKey(name);

        public static bool Contains<T>(CaseInsensitiveString name)
        {
            if (_variables.ContainsKey(name))
            {
                var obj = _variables[name]; //get instance without casting
                return obj.GetType() == typeof(T);
            }

            return false;
        }

        public static IEnumerable<T> GetAll<T>()
        {
            foreach (var item in _variables)
            {
                if (item.Value is T)
                    yield return (T)item.Value;
            }
        }

        public static T GetInstance<T>(CaseInsensitiveString name)
        {
            if (!_variables.ContainsKey(name)) throw new Exception($"'{name}' is not declared");

            return (T)_variables[name];
        }
    }
}