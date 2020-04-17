using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.Commands
{
    public class InstantActionCommand : Command
    {
        private Action action;

        public InstantActionCommand(Action action)
        {
            this.action = action;
        }

        protected override void OnRun()
        {
            action();
            Finish();
        }
    }
}
