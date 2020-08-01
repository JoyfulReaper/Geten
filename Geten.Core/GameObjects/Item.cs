/*
MIT License

Copyright(c) 2020 Kyle Givler

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using Geten.Core.Parsers.Script.Syntax;
using System;
using System.Collections.Generic;

namespace Geten.Core.GameObjects
{
	/// <summary>
	/// Represents an item
	/// </summary>
	public class Item : GameObject
	{
		public override List<string> GetPropertyPositionMap()
		{
			var map = base.GetPropertyPositionMap();
			map.Add("pluralName");
			map.Add("description");
			map.Add("visible");
			map.Add("obtainable");
			return map;
		}

		public override void Initialize(PropertyList properties)
		{
			base.Initialize(properties);

			AddDefaultValue("visible", true);
			AddDefaultValue("obtainable", true);
			AddDefaultValue("quantity", 1);

			var pluralName = GetProperty<string>("PluralName");
			if (string.IsNullOrEmpty(pluralName))
				SetProperty("PluralName", Name + "s");
		}

		/// <summary>
		/// Use this item
		/// </summary>
		public virtual void Use()
		{
			throw new NotImplementedException();
		}
	}
}