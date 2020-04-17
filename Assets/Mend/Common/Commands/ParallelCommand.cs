using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.Commands
{
    public class ParallelCommand : CompositeCommand
    {
        private Command[] children;
        private int finishedChildren = 0;

        public ParallelCommand(Command[] commands):base(commands)
        {
            this.children = commands.ToArray();
        }

        protected override void OnRun()
        {
            foreach (var command in children)
            {
                command.Run(ChildFinished);
            }
            CheckIfOver();
        }

        private void ChildFinished(Command c)
        {
            finishedChildren++;
            CheckIfOver();
        }

        private void CheckIfOver()
        {
            if(finishedChildren == children.Length)
            {
                Finish();
            }
        }
    }
}
