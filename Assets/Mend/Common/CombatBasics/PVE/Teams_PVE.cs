using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.CombatBasics
{
    public enum PVETeamCode
    {
        Player, Enemy
    }
    public partial class Team
    {
        //Use this for a simple all players units vs all Enemy units
        private static readonly Team PlayerPVE = new Team();
        private static readonly Team EnemyPVE = new Team();

        public static Team FromCode(PVETeamCode code)
        {
            switch (code)
            {
                case PVETeamCode.Player:
                    return PlayerPVE;
                case PVETeamCode.Enemy:
                    return EnemyPVE;
                default:
                    throw new NotImplementedException();
            }
        }
    }
    public static class PVETeamExtensions
    {
        public static Team ToTeam(this PVETeamCode code)
        {
            return Team.FromCode(code);
        }
    }
}
