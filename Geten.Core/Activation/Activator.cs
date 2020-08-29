using System;

namespace Geten.Core.Activation
{
	public class Activator<ActivationStrategy>
		where ActivationStrategy : IActivationStrategy, new()
	{
		public static Activator<ActivationStrategy> Instance = new Activator<ActivationStrategy>();
		private readonly ActivationStrategy _strategy = new ActivationStrategy();

		public object CreateInstance(Type type, object[] args = null)
		{
			return _strategy.Activate(type, args);
		}

		public T CreateInstance<T>(object[] args = null)
		{
			return (T)CreateInstance(typeof(T), args);
		}
	}
}