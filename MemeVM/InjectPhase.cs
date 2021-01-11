using Confuser.Core;
using dnlib.DotNet;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MemeVM
{
    public class InjectPhase : ProtectionPhase
    {
        public InjectPhase(ConfuserComponent parent) : base(parent)
        {
        }

        public override string Name => "MemeVM.Injection";
        public override ProtectionTargets Targets => ProtectionTargets.Methods;

        protected override void Execute(ConfuserContext context, ProtectionParameters parameters)
        {
            if (!parameters.Targets.Any())
                return;

            var current = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException();
            var runtimePath = Path.Combine(current, "MemeVM.Runtime.dll");
            var newPath = Path.Combine(context.OutputDirectory, "MemeVM.Runtime.dll");

            var cliPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Confuser.CLI.exe");

            Directory.CreateDirectory(Path.GetDirectoryName(newPath));
            File.Copy(runtimePath, newPath, true);

            if (!File.Exists(cliPath))
            {
                context.Logger.Warn("Confuser.CLI.exe could not be found. MemeVM.Runtime.dll cannot be protected.");
            }
            else
            {
                //the protection can break the vm
                context.Logger.Info("Protecting VM runtime...");
                var info = new ProcessStartInfo
                {
                    FileName = cliPath,
                    Arguments = "-n -o " + context.OutputDirectory + " " + newPath,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(info)?.WaitForExit();
            }

            Context.RuntimeModule = ModuleDefMD.Load(newPath);
            Context.Entry = Context.RuntimeModule.Types.Single(t => t.IsPublic).Methods[0];
        }
    }
}