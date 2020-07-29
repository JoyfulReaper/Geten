using System;

namespace Geten.Core
{
	[AttributeUsage(AttributeTargets.Class)]
	public class GameObjectKindAttribute : Attribute
	{
		public GameObjectKindAttribute(GameObjectKind kind)
		{
			Kind = kind;
		}

		public GameObjectKind Kind { get; }
	}
}