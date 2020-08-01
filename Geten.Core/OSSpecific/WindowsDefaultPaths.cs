using System;
using System.IO;
using CrossPlattformUtils;

namespace Geten.Core.OSSpecific
{
	[PlattformImplementation(Platform.Windows)]
	public class WindowsDefaultPaths : IDefaultPaths
	{
		public string GameDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
			"Geten", "Games");
	}
}