using System;
using System.Collections.Generic;
using System.IO;
using Geten.Core.Parsers.Script.Syntax;
using Geten.Runtime.Binary;

namespace Geten.Runtime
{
	public class VM
	{
		private BinaryGameDefinitionFile _file;

		public void Load(BinaryGameDefinitionFile file)
		{
			_file = file;
		}

		public void Run()
		{
			var body = _file.GetBodyOfSection("Data");
			var br = new BinaryReader(new MemoryStream(body));
			var instructions = new List<Instruction>();

			Instruction instr;
			do
			{
				instr = EncodeInstruction(br);
				instructions.Add(instr);
			} while (instr.OpCode != OpCode.NOP);

			RunInstructions(instructions);
		}

		private Instruction EncodeInstruction(BinaryReader br)
		{
			var op = (OpCode)br.ReadByte();

			if (op.IsDefinition())
			{
				var props = ""; //ReadProperties(br);

				return Instruction.Create(op, props);
			}
			else
			{
				// encode other instructions
			}

			return Instruction.Create(op);
		}

		private void RunInstructions(List<Instruction> instructions)
		{
			foreach (var ins in instructions)
			{
			}
		}
	}
}