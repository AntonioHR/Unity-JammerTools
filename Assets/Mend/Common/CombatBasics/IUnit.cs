using UnityEngine;

namespace PointNSheep.Common.CombatBasics
{
    public interface IUnit
    {
        IHealth Health { get; }
        ITeam Team { get; }
        Transform Transform { get; }
    }
}