namespace PointNSheep.Common.CombatBasics
{
    public interface ITeam
    {
        bool IsEnemyOf(ITeam other);
    }
}