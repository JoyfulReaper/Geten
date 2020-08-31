using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Geten.Core
{
	public static class SymbolTable
	{
		private static readonly ConcurrentDictionary<CaseSensisitiveString, object> objects = new ConcurrentDictionary<CaseSensisitiveString, object>();

		public static void Add(CaseSensisitiveString name, object instance)
		{
			if (objects.ContainsKey(name)) throw new Exception($"'{name}' is already declared");

			objects.TryAdd(name, instance);
		}

		public static void ClearAllSymbols() => objects.Clear();

		public static bool Contains(CaseSensisitiveString name) => objects.ContainsKey(name);

		public static bool Contains<T>(CaseSensisitiveString name)
		{
			if (objects.ContainsKey(name))
			{
				var obj = objects[name]; //get instance without casting
				return obj.GetType() == typeof(T);
			}

			return false;
		}

		public static bool ContainsGameObject(GameObject obj) => objects.Contains(new KeyValuePair<CaseSensisitiveString, object>(obj.Name, obj));

		public static IEnumerable<T> GetAll<T>()
		{
			foreach (var item in objects)
			{
				if (item.Value is T t)
					yield return t;
			}
		}

		public static CaseSensisitiveString[] GetAllNames()
		{
			return objects.Keys.ToArray();
		}

		public static T GetInstance<T>(CaseSensisitiveString name)
		{
			if (!objects.ContainsKey(name)) throw new Exception($"'{name}' is not declared");

			return (T)objects[name];
		}
	}
}