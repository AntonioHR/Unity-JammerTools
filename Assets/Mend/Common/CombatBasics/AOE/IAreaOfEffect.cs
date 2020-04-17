using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.CombatBasics
{
    public interface IAreaOfEffect
    {
        void TryHitUnit(IUnit unit);
        void Reset();
    }
}
