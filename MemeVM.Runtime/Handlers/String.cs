using MemeVM.Runtime.Engine;
using System.IO;
using System.Text;

namespace MemeVM.Runtime.Handlers
{
    internal class String : IHandler
    {
        public OpCode Handles => OpCode.String;

        public void Handle(VM machine, Body body, Instruction instruction) =>
            machine.Stack.Push(instruction.Operand);

        public Instruction Deserialize(BinaryReader reader)
        {
            var len = reader.ReadInt32();

            return new Instruction(OpCode.String, Encoding.UTF8.GetString(reader.ReadBytes(len)));
        }
    }
}