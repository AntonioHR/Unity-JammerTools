using JammerTools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JammerTools.Commands
{
    public class DelayedCommand : CompositeCommand
    {
        float waitTime;
        private Command command;

        public DelayedCommand(float waitTime, Command command):base(command)
        {
            this.waitTime = waitTime;
            this.command = command;
        }

        protected override void OnRun()
        {
            Wait.ForSecondsThenDo(waitTime, RunCommand);
        }

        private void RunCommand()
        {
            command.Run(Finish);
        }
    }
}
