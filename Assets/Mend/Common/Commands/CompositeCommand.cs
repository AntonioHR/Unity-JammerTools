using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.Commands
{
    public abstract class CompositeCommand : Command
    {
        public CompositeCommand(IEnumerable<Command> children)
        {
            foreach (var child in children)
            {
                DebugRegisterChild(child);
            }
        }
        public CompositeCommand(params Command[] children)
        {
            foreach (var child in children)
            {
                DebugRegisterChild(child);
            }
        }
        public CompositeCommand(Command child)
        {
            DebugRegisterChild(child);
        }
    }
}
