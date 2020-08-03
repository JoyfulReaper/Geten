using System;
using System.IO;
using System.Linq;
using Geten.Core.Parsers.Script;
using Geten.Runtime;

namespace GetenCompiler
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				var filename = args[0];

				var p = new ScriptParser();
				var result = p.Parse(File.ReadAllText(filename));

				if (p.Diagnostics.Any())
				{
					foreach (var d in p.Diagnostics)
					{
						Console.WriteLine(d);
					}
					return;
				}
				else
				{
					result.Accept(new CompilationVisitor(p.Diagnostics));
				}
			}
			else
			{
				Console.WriteLine("No Script given to compile");
			}
		}
	}
}