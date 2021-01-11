using MemeVM.Runtime.Engine;
using System.IO;

namespace MemeVM.Runtime.Handlers
{
    internal class Stloc : IHandler
    {
        public OpCode Handles => OpCode.Stloc;

        public void Handle(VM machine, Body body, Instruction instruction) =>
            machine.Locals.Set((short)instruction.Operand, machine.Stack.Pop());

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Stloc, reader.ReadInt16());
    }
}