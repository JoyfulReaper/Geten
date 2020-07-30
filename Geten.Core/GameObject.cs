using Geten.Core.Parsers.Script.Syntax;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Geten.Core
{
	public abstract class GameObject : DynamicObject, IEnumerable
	{
		private readonly ConcurrentDictionary<CaseInsensitiveString, object> _defaultValues = new ConcurrentDictionary<CaseInsensitiveString, object>();

		//property bag for mutable properties by script
		private readonly ConcurrentDictionary<CaseInsensitiveString, object> _properties = new ConcurrentDictionary<CaseInsensitiveString, object>();

		private readonly List<string> _propertyMap = new List<string>();

		public string Description
		{
			get { return GetProperty<string>(nameof(Description)); }
			set { SetProperty(nameof(Description), value); }
		}

		public string Name
		{
			get { return GetProperty<string>(nameof(Name)); }
			set { SetProperty(nameof(Name), value); }
		}

		public int PropertyCount => _properties.Count;

		public static T Create<T>(object arg)
			where T : GameObject
		{
			var instance = ObjectFactory.Create<T>(arg);
			instance.Initialize(new PropertyList());

			return instance;
		}

		public static T Create<T>(string name, PropertyList properties)
			where T : GameObject
		{
			if (properties == null)
				properties = new PropertyList();

			var instance = ObjectFactory.Create<T>(new object[] { name, properties });
			instance.Initialize(properties);

			return instance;
		}

		public void Add(string name, object value)
		{
			SetProperty(name, value);
		}

		public void AddDefaultValue(string name, object value)
		{
			_defaultValues.TryAdd(name, value);
		}

		public ConcurrentDictionary<CaseInsensitiveString, object> GetAllProperties() => _properties;

		public override IEnumerable<string> GetDynamicMemberNames()
		{
			foreach (var prop in _properties)
			{
				yield return prop.Key;
			}
		}

		public IEnumerator GetEnumerator()
		{
			return _properties.GetEnumerator();
		}

		public T GetProperty<T>(string name)
		{
			if (_properties.ContainsKey(name))
			{
				return (T)_properties[name];
			}
			else if (_defaultValues.ContainsKey(name))
			{
				return (T)_defaultValues[name];
			}

			return default;
		}

		public virtual List<string> GetPropertyPositionMap()
		{
			return new List<string> { "name" };
		}

		public bool HasProperty(string property) => _properties.ContainsKey(property);

		public virtual void Initialize(PropertyList properties)
		{
			foreach (var item in properties)
			{
				SetProperty(item.Key, properties[item.Key]);
			}
		}

		public void MatchPropertyList(PropertyList list)
		{
			foreach (var p in list)
			{
				SetProperty(p.Key, list[p.Key]);
			}
		}

		public void SetProperty(string name, object value)
		{
			if (_properties.ContainsKey(name))
			{
				_properties[name] = value;
			}
			else
			{
				_properties.TryAdd(name, value);
			}
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append('[').Append(GetType()).Append("] ");
			foreach (var prop in _properties)
			{
				sb.Append(prop.Key).Append(": ").Append(prop.Value).Append(' ');
			}
			return sb.ToString();
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = GetProperty<object>(binder.Name);
			return true;
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			SetProperty(binder.Name, value);
			return true;
		}
	}
}