using System;
using UnityEngine;
using UnityEngine.Events;

namespace PointNSheep.Common.CombatBasics
{
    /// <summary>
    /// This is for when you want to implement health creation instead of it being set by inspector
    /// </summary>
    public abstract class PVEUnitCustom : MonoUnitBase, IUnit
    {
        //Params
        [SerializeField]
        private PVETeamCode team;
        public override ITeam Team => team.ToTeam();

    }
}