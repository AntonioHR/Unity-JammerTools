using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.CombatBasics
{
    public partial class Team : ITeam
    {
        private HashSet<ITeam> allies;
        private HashSet<ITeam> enemies;

        public Team(bool defaultsOthersToEnemy = true, bool isOwnEnemy = false)
        {
            this.isOwnEnemy = isOwnEnemy;
            this.defaultsOthersToEnemy = defaultsOthersToEnemy;
            allies = new HashSet<ITeam>();
            enemies = new HashSet<ITeam>();
        }
        public Team(bool defaultsOthersToEnemy, IEnumerable<ITeam> exceptionTeams, bool isOwnEnemy = false)
        {
            this.isOwnEnemy = isOwnEnemy;
            this.defaultsOthersToEnemy = defaultsOthersToEnemy;

            if(defaultsOthersToEnemy)
            {
                allies = new HashSet<ITeam>(exceptionTeams);
                enemies = new HashSet<ITeam>();
            }
            else
            {
                allies = new HashSet<ITeam>();
                enemies = new HashSet<ITeam>(exceptionTeams);
            }
        }

        private bool isOwnEnemy;
        private bool defaultsOthersToEnemy;

        public bool IsEnemyOf(ITeam other)
        {
            if (other == this)
                return isOwnEnemy;

            if(defaultsOthersToEnemy)
            {
                return !allies.Contains(other);
            } else
            {
                return enemies.Contains(other);
            }
        }
    }
}
