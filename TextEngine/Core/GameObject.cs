using Geten.Parsers.Script.Syntax;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Text;

namespace Geten.Core
{
    public abstract class GameObject : DynamicObject, IEnumerable
    {
        //property bag for mutable properties by script
        private readonly ConcurrentDictionary<CaseInsensitiveString, object> _properties = new ConcurrentDictionary<CaseInsensitiveString, object>();

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            foreach (var prop in _properties)
            {
                yield return prop.Key;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetProperty<Object>(binder.Name);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            SetProperty(binder.Name, value);
            return true;
        }

        public virtual void Initialize(PropertyList properties)
        {
            foreach (var item in properties)
            {
                SetProperty(item.Key.Value.ToString(), properties[item.Key.Value.ToString()]);
            }
        }

        public bool HasProperty(string property) => _properties.ContainsKey(property);
        

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

        public static T Create<T>(params object[] args)
                                            where T : GameObject
        {
            return ObjectFactory.Create<T>(args);
        }

        public static T Create<T>(PropertyList props)
                                            where T : GameObject
        {
            var instance = ObjectFactory.Create<T>();
            instance.MatchPropertyList(props);

            return instance;
        }

        public void Add(string name, object value)
        {
            SetProperty(name, value);
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

            return default;
        }

        public void MatchPropertyList(PropertyList list)
        {
            foreach (var p in list)
            {
                SetProperty(p.Key.Text, list[p.Key.Text]);
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
            StringBuilder sb = new StringBuilder();
            sb.Append($"[{this.GetType()}] ");
            foreach(var prop in _properties)
            {
                sb.Append($"{prop.Key}: {prop.Value} ");
            }
            return sb.ToString();
        }
    }
}