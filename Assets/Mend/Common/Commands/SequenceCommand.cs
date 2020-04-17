using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.Commands
{
    public class SequenceCommand : CompositeCommand
    {
        private Command[] children;
        private int currentChild = 0;

        public SequenceCommand(Command[] commands) : base(commands)
        {
            this.children = commands.ToArray();
        }

        protected override void OnRun()
        {
            CheckAndRunNext();
        }

        private void CheckAndRunNext()
        {
            if(currentChild >= children.Length)
            {
                Finish();
            } else
            {
                children[currentChild++].Run(CheckAndRunNext);
            }
        }
    }
}
