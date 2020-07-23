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

using Geten.Core;
using Geten.GameObjects;
using Geten.MapItems;
using Geten.Parsers.Script;
using System;
using System.IO;

namespace Geten
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(@"..\..\..\SampleGame");
            Console.WriteLine(Directory.GetCurrentDirectory());
            ShowIntro();

            string script = System.IO.File.ReadAllText(@"Demo.script");
            ScriptParser scriptParser = new ScriptParser();
            var result = scriptParser.Parse(script);
            result.Accept(new EvaluationVisitor(scriptParser.Diagnostics));

            GreedyWrap wrapper = new GreedyWrap(Console.WindowWidth);
            TextEngine.StartGame();
            while (!TextEngine.GameOver)
            {
                wrapper.LineWidth = Console.WindowWidth;
                while (TextEngine.HasMessage())
                {
                    Console.WriteLine(wrapper.LineWrap((TextEngine.GetMessage())));
                }

                Console.Write("\nEnter command: ");
                string input = Console.ReadLine();
                TextEngine.ProccessCommand(input);
            }
        }

        private static void ShowIntro()
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("\nGeten - General Text Adventure Engine\n");
            Console.WriteLine("----------------------------------------------------\n\n");
        }
    }
}