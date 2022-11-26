using NetCoreAudio.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NetCoreAudio.Players
{
    internal class MacPlayer : UnixPlayerBase, IPlayer
    {
        protected override string GetBashCommand(string fileName)
        {
            return "afplay";
        }

        public override Task SetVolume(byte percent, string target = null)
        {
            if (percent > 100)
                throw new ArgumentOutOfRangeException(nameof(percent), "Percent can't exceed 100");

            Process tempProcess;
            
            if(target != null)
                tempProcess = StartBashProcess($"osascript -e \"tell application \"{target}\" to set sound volume to {percent}\"");
            else
                tempProcess = StartBashProcess($"osascript -e \"set volume output volume {percent}\"");

            tempProcess.WaitForExit();

            return Task.CompletedTask;
        }
    }
}
