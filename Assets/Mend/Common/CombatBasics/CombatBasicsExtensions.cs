using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.CombatBasics
{
    public static class CombatBasicsExtensions
    {
        public static float Percent(this IHealth health)
        {
            return (float)health.Current / (float)health.Max;
        }
    }
}
