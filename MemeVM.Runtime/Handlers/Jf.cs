using MemeVM.Runtime.Engine;
using System.IO;

namespace MemeVM.Runtime.Handlers
{
    internal class Jf : IHandler
    {
        public OpCode Handles => OpCode.Jf;

        public void Handle(VM machine, Body body, Instruction instruction)
        {
            var value = machine.Stack.Pop();
            if (value == null || !(bool)value)
                machine.Ip = (int)instruction.Operand;
        }

        public Instruction Deserialize(BinaryReader reader) =>
            new Instruction(OpCode.Jf, reader.ReadInt32());
    }
}