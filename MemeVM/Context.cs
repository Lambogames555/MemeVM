using dnlib.DotNet;
using MemeVM.Translation;
using System;
using System.Collections.Generic;

namespace MemeVM
{
    internal static class Context
    {
        internal static IMethod Entry;
        internal static ModuleDef RuntimeModule;
        internal static readonly Random Random = new Random();
        internal static readonly Dictionary<ModuleDef, VMBody> Bodies = new Dictionary<ModuleDef, VMBody>();
    }
}