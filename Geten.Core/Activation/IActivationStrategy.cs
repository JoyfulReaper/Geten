using System;

namespace Geten.Core.Activation
{
	public interface IActivationStrategy
	{
		object Activate(Type type, object[] args);
	}
}