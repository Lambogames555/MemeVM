using MemeVM.Runtime.Engine;
using System.IO;

namespace MemeVM.Runtime.Handlers
{
    internal class Jmp : IHandler
    {
        public OpCode Handles => OpCode.Jmp;

        public void Handle(VM machine, Body body, Instruction instruction) =>
            machine.Ip = (int)instruction.Operand;

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Jmp, reader.ReadInt32());
    }
}