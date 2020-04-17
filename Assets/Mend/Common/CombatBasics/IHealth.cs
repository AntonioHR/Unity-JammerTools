namespace PointNSheep.Common.CombatBasics
{
    public delegate void HealthEvent();
    public delegate void HealthDeltaEvent(int delta);
    public interface IHealth
    {
        event HealthEvent Died;
        event HealthEvent InvulnerableChanged;
        event HealthEvent Changed;
        event HealthDeltaEvent TookDamage;
        event HealthDeltaEvent Healed;
        bool IsAlive { get; }
        bool IsInvulnerable { get; }
        bool IsHittable { get; }
        int Current { get; }
        int Max { get; }

        void ReceiveDamage(int damage);
        void ReceiveHeal(int heal);
        void AddInvulnerable();
        void RemoveInvulnerable();

        void ResetToMax();
        void ResetTo(int current, int max);
    }
}