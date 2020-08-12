using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geten.Core.Event
{
	public class EventHub
	{
		internal List<Handler> _handlers = new List<Handler>();
		internal object _locker = new object();
		private static EventHub @default;

		public static EventHub Default => @default ??= new EventHub();

		public bool Exists<T>()
		{
			return Exists<T>(this);
		}

		public bool Exists<T>(object subscriber)
		{
			return Exists(typeof(T).Name, subscriber);
		}

		public bool Exists(string name, object subscriber)
		{
			lock (_locker)
			{
				foreach (var h in _handlers)
				{
					if (Equals(h.Sender.Target, subscriber) &&
						 name == h.Type)
					{
						return true;
					}
				}
			}

			return false;
		}

		public bool Exists<T>(object subscriber, Action<T> handler)
		{
			lock (_locker)
			{
				foreach (var h in _handlers)
				{
					if (Equals(h.Sender.Target, subscriber) &&
						 typeof(T).Name == h.Type &&
						 h.Action.Equals(handler))
					{
						return true;
					}
				}
			}

			return false;
		}

		public void Publish<T>(T data = default)
		{
			foreach (var handler in GetAliveHandlers<T>())
			{
				switch (handler.Action)
				{
					case Action<T> action:
						action(data);
						break;

					case Func<T, Task> func:
						func(data);
						break;
				}
			}
		}

		public void Publish<T>(string name, T data = default)
		{
			foreach (var handler in GetAliveHandlers(name))
			{
				switch (handler.Action)
				{
					case Action<T> action:
						action(data);
						break;

					case Func<T, Task> func:
						func(data);
						break;
				}
			}
		}

		public async Task PublishAsync<T>(T data = default)
		{
			foreach (var handler in GetAliveHandlers<T>())
			{
				switch (handler.Action)
				{
					case Action<T> action:
						action(data);
						break;

					case Func<T, Task> func:
						await func(data).ConfigureAwait(false);
						break;
				}
			}
		}

		public async Task PublishAsync<T>(string name, T data = default)
		{
			foreach (var handler in GetAliveHandlers(name))
			{
				switch (handler.Action)
				{
					case Action<T> action:
						action(data);
						break;

					case Func<T, Task> func:
						await func(data).ConfigureAwait(false);
						break;
				}
			}
		}

		/// <summary>
		///     Allow subscribing directly to this Hub.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="handler"></param>
		public void Subscribe<T>(Action<T> handler)
		{
			Subscribe(this, handler);
		}

		public void Subscribe<T>(object subscriber, Action<T> handler)
		{
			SubscribeDelegate<T>(subscriber, handler);
		}

		public void Subscribe<T>(Func<T, Task> handler)
		{
			Subscribe(this, handler);
		}

		public void Subscribe<T>(object subscriber, Func<T, Task> handler)
		{
			SubscribeDelegate<T>(subscriber, handler);
		}

		public void Subscribe<T>(string name, object subscriber, Func<T, Task> handler)
		{
			SubscribeDelegate(name, subscriber, handler);
		}

		/// <summary>
		///     Allow unsubscribing directly to this Hub.
		/// </summary>
		public void Unsubscribe()
		{
			Unsubscribe(this);
		}

		public void Unsubscribe(object subscriber)
		{
			lock (_locker)
			{
				var query = _handlers.Where(a => !a.Sender.IsAlive ||
												a.Sender.Target.Equals(subscriber));

				foreach (var h in query.ToList())
					_handlers.Remove(h);
			}
		}

		/// <summary>
		///     Allow unsubscribing directly to this Hub.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void Unsubscribe<T>()
		{
			Unsubscribe<T>(this);
		}

		/// <summary>
		///     Allow unsubscribing directly to this Hub.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="handler"></param>
		public void Unsubscribe<T>(Action<T> handler)
		{
			Unsubscribe(this, handler);
		}

		public void Unsubscribe<T>(object subscriber, Action<T> handler = null)
		{
			lock (_locker)
			{
				var query = _handlers.Where(a => !a.Sender.IsAlive ||
												(a.Sender.Target.Equals(subscriber) && a.Type == typeof(T).Name));

				if (handler != null)
					query = query.Where(a => a.Action.Equals(handler));

				foreach (var h in query.ToList())
					_handlers.Remove(h);
			}
		}

		private List<Handler> GetAliveHandlers<T>()
		{
			PruneHandlers();
			return _handlers.Where(h => h.Type == typeof(T).Name).ToList();
		}

		private List<Handler> GetAliveHandlers(string name)
		{
			PruneHandlers();
			return _handlers.Where(h => h.Type == name).ToList();
		}

		private void PruneHandlers()
		{
			lock (_locker)
			{
				for (var i = _handlers.Count - 1; i >= 0; --i)
				{
					if (!_handlers[i].Sender.IsAlive)
						_handlers.RemoveAt(i);
				}
			}
		}

		private void SubscribeDelegate<T>(object subscriber, Delegate handler)
		{
			var item = new Handler
			{
				Action = handler,
				Sender = new WeakReference(subscriber),
				Type = typeof(T).Name
			};

			lock (_locker)
			{
				_handlers.Add(item);
			}
		}

		private void SubscribeDelegate(string name, object subscriber, Delegate handler)
		{
			var item = new Handler
			{
				Action = handler,
				Sender = new WeakReference(subscriber),
				Type = name
			};

			lock (_locker)
			{
				_handlers.Add(item);
			}
		}

		internal class Handler
		{
			public Delegate Action { get; set; }
			public WeakReference Sender { get; set; }
			public string Type { get; set; }
		}
	}
}