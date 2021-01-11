using MemeVM.Runtime.Engine;
using System.IO;

namespace MemeVM.Runtime.Handlers
{
    internal class Jt : IHandler
    {
        public OpCode Handles => OpCode.Jt;

        public void Handle(VM machine, Body body, Instruction instruction)
        {
            var value = machine.Stack.Pop();
            if (value != null && (bool)value)
                machine.Ip = (int)instruction.Operand;
        }

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Jt, reader.ReadInt32());
    }
}