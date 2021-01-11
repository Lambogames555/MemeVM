using MemeVM.Runtime.Engine;
using System.IO;

namespace MemeVM.Runtime
{
    internal interface IHandler
    {
        OpCode Handles { get; }

        void Handle(VM machine, Body body, Instruction instruction);

        Instruction Deserialize(BinaryReader reader);
    }
}