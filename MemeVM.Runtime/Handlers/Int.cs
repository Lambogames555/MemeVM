using MemeVM.Runtime.Engine;
using System.IO;

namespace MemeVM.Runtime.Handlers
{
    internal class Int : IHandler
    {
        public OpCode Handles => OpCode.Int32;

        public void Handle(VM machine, Body body, Instruction instruction) =>
            machine.Stack.Push(instruction.Operand);

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Int32, reader.ReadInt32());
    }
}