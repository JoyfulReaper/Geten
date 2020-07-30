using System.Collections.Generic;

namespace Geten.Core.Parsers.Script.Syntax
{
	public class PropertyList : Dictionary<string, object>
	{
		public object this[CaseInsensitiveString key]
		{
			get
			{
				foreach (var kvp in this)
				{
					if (kvp.Key == key)
					{
						return kvp.Value;
					}
				}

				return null;
			}
			set
			{
				this[key] = value;
			}
		}
	}
}