using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JammerTools.Commands
{
    public class ActionWithCallbackCommand : Command
    {
        private Action<Action> action;

        public ActionWithCallbackCommand(Action<Action> action)
        {
            this.action = action;
        }

        protected override void OnRun()
        {
            action(Finish);
        }
    }
}
