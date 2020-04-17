using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace PointNSheep.Common.CombatBasics
{
    public class PVETeamInstaller : MonoInstaller
    {
        public PVETeamCode teamCode;
        public override void InstallBindings()
        {
            Container.Bind<ITeam>().FromInstance(teamCode.ToTeam());
        }
    }
}
