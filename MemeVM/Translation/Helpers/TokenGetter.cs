﻿using dnlib.DotNet;
using dnlib.DotNet.Writer;

namespace MemeVM.Translation.Helpers
{
    internal static class TokenGetter
    {
        internal static ModuleWriterBase Writer;

        internal static int GetMdToken(IMemberDef member) =>
            Writer.Module == member.Module ? Writer.Metadata.GetToken(member).ToInt32() : member.MDToken.ToInt32();
    }
}