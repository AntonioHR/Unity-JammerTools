using PointNSheep.Common.Interactables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.CombatBasics
{
    public abstract class AreaOfEffect : IAreaOfEffect
    {
        private List<IUnit> hitUnits = new List<IUnit>();

        private ITeam myTeam;
        private bool hitsEnemies;
        private bool hitsAllies;

        protected AreaOfEffect(ITeam myTeam, bool hitsEnemies, bool hitsAllies)
        {
            this.myTeam = myTeam;
            this.hitsEnemies = hitsEnemies;
            this.hitsAllies = hitsAllies;
        }

        public void Reset()
        {
            hitUnits.Clear();
        }
        public void TryHitUnit(IUnit unit)
        {
            if (!CanHit(unit))
                return;
            if (hitUnits.Contains(unit))
                return;

            hitUnits.Add(unit);
            Hit(unit);
        }

        private bool CanHit(IUnit unit)
        {
            if (unit.Team.IsEnemyOf(myTeam))
                return hitsEnemies;
            else
                return hitsAllies;
        }

        protected abstract void Hit(IUnit unit);
    }
}

