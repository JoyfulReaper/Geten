using System;
using System.Linq;
using System.Reflection.Emit;

namespace Geten.Core.Activation.Strategies
{
	public class DynamicDelegateActivation : IActivationStrategy
	{
		public object Activate(Type type, object[] args)
		{
			var argTypes = args.Select(_ => _.GetType()).ToArray();
			var ctor = type.GetConstructor(argTypes);
			var dm = new DynamicMethod("GetInstance", type, argTypes);

			var il = dm.GetILGenerator();
			il.Emit(OpCodes.Newobj, ctor);
			il.Emit(OpCodes.Ret);

			var del = dm.CreateDelegate(typeof(Func<object>));
			return del.DynamicInvoke(args);
		}
	}
}