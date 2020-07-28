using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Geten.Core
{
    public static class SymbolTable
    {
        private static readonly ConcurrentDictionary<CaseInsensitiveString, object> _objects = new ConcurrentDictionary<CaseInsensitiveString, object>();

        public static void Add(CaseInsensitiveString name, object instance)
        {
            if (_objects.ContainsKey(name)) throw new Exception($"'{name}' is already declared");

            _objects.TryAdd(name, instance);
        }

        public static bool Contains(CaseInsensitiveString name) => _objects.ContainsKey(name);

        public static bool Contains<T>(CaseInsensitiveString name)
        {
            if (_objects.ContainsKey(name))
            {
                var obj = _objects[name]; //get instance without casting
                return obj.GetType() == typeof(T);
            }

            return false;
        }

        public static bool ContainsGameObject(GameObject obj) => _objects.Contains(new KeyValuePair<CaseInsensitiveString, object>(obj.Name, obj));

        public static T GetInstance<T>(CaseInsensitiveString name)
        {
            if (!_objects.ContainsKey(name)) throw new Exception($"'{name}' is not declared");

            return (T)_objects[name];
        }

        public static IEnumerable<T> GetAll<T>()
        {
            foreach (var item in _objects)
            {
                if (item.Value is T)
                    yield return (T)item.Value;
            }
        }

        public static void ClearAllSymbols() => _objects.Clear();
    }
}