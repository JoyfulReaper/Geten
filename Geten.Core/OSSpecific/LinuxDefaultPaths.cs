using CrossPlattformUtils;

namespace Geten.Core.OSSpecific
{
	[PlattformImplementation(Platform.Linux)]
	public class LinuxDefaultPaths : IDefaultPaths
	{
		public string GameDirectory => "/usr/share/Geten/";
	}
}