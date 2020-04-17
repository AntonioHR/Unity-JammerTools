using System;
using UnityEngine;
using UnityEngine.Events;

namespace PointNSheep.Common.CombatBasics
{
    public class PVEUnit : MonoHealthUnitBase, IUnit
    {
        //Params
        [SerializeField]
        private PVETeamCode team;
        public override ITeam Team => team.ToTeam();

    }
}