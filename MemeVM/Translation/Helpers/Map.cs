using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;

namespace MemeVM.Translation.Helpers
{
    internal static class Map
    {
        static Map()
        {
            foreach (var type in typeof(Map).Assembly.DefinedTypes)
            {
                if (type.IsInterface)
                    continue;

                if (!typeof(IHandler).IsAssignableFrom(type))
                    continue;

                var instance = (IHandler)Activator.CreateInstance(type);

                foreach (var regular in instance.Translates)
                    OpCodeToHandler.Add(regular, instance);

                VMOpCodeToHandler.Add(instance.Output, instance);
            }
        }

        private static readonly Dictionary<OpCode, IHandler> OpCodeToHandler = new Dictionary<OpCode, IHandler>();
        private static readonly Dictionary<VMOpCode, IHandler> VMOpCodeToHandler = new Dictionary<VMOpCode, IHandler>();

        internal static IHandler Lookup(OpCode opcode) =>
            OpCodeToHandler.ContainsKey(opcode) ? OpCodeToHandler[opcode] : null;

        internal static IHandler Lookup(VMOpCode opcode) =>
            VMOpCodeToHandler.ContainsKey(opcode) ? VMOpCodeToHandler[opcode] : null;
    }
}