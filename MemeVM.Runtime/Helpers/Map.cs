using System;
using System.Collections.Generic;

namespace MemeVM.Runtime.Helpers
{
    internal static class Map
    {
        static Map()
        {
            foreach (var type in typeof(Map).Module.GetTypes())
            {
                if (type.IsInterface)
                    continue;

                if (!typeof(IHandler).IsAssignableFrom(type))
                    continue;

                var instance = (IHandler)Activator.CreateInstance(type);
                OpCodeToHandler.Add(instance.Handles, instance);
            }
        }

        private static readonly Dictionary<OpCode, IHandler> OpCodeToHandler = new Dictionary<OpCode, IHandler>();

        internal static IHandler Lookup(OpCode code) =>
            OpCodeToHandler[code];
    }
}