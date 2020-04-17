using PointNSheep.Common.Interactables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.CombatBasics
{
    public class UnitDetector : ObjectCheckArea<IUnit>
    {

        public PVETeamCode teamToCheck;

        public override bool IsValid(IUnit unit)
        {
            return unit.Team == teamToCheck.ToTeam();
        }

    }
}
