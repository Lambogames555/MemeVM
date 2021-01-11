using MemeVM.Runtime.Engine;
using System.Collections.Generic;
using System.Reflection;

namespace MemeVM.Runtime
{
    internal static class Dispatcher
    {
        private static readonly Dictionary<Assembly, Body> Bodies = new Dictionary<Assembly, Body>();

        internal static object Run(Assembly asm, int index, object[] parameters)
        {
            var body = GetBody(asm);
            body.CurrentAssembly = asm;

            var instance = new VM(body.GetMethod(index).ToArray(), body, parameters);
            return instance.Run();
        }

        internal static Body GetBody(Assembly asm)
        {
            if (!Bodies.ContainsKey(asm))
                Bodies.Add(asm, new Body(asm.GetManifestResourceStream(" ")));

            return Bodies[asm];
        }
    }
}