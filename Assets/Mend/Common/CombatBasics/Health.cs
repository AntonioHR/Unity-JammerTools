using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PointNSheep.Common.CombatBasics
{
    public class Health : IHealth
    {
        public event HealthEvent Died;
        public event HealthEvent Changed;
        public event HealthDeltaEvent TookDamage;
        public event HealthDeltaEvent Healed;
        public event HealthEvent InvulnerableChanged;

        private int shields;

        [Serializable]
        public class Setup
        {
            public int starting;
            public int max;
        }
        public Health(Setup setup) : this(setup.starting, setup.max) { }
        public Health(int max) : this(max, max) { }
        public Health(int starting, int max)
        {
            Current = starting;
            Max = max;
            IsAlive = true;

            Debug.Assert(Max > 0, "Max Health must be > 0");
            Debug.Assert(starting > 0, "Starting Health must be > 0");
            Debug.Assert(Current <= Max, "Starting Health must be <= Max Health");
        }
        

        public bool IsAlive { get; private set; }
        public int Current { get; private set; }
        public int Max{ get; private set; }

        public bool IsInvulnerable => shields > 0;
        public bool IsHittable => IsAlive && !IsInvulnerable;

        public void ReceiveDamage(int damage)
        {
            //Debug.Assert(damage > 0, "Can't take damage <= 0");
            if (!IsHittable || damage <= 0)
                return;
            SetHealth(Current - damage, out int delta);
        }
        public void ReceiveHeal(int heal)
        {
            //Debug.Assert(heal > 0, "Can't heal amount <= 0");
            if (!IsAlive || heal <= 0)
                return;
            SetHealth(Current + heal, out int delta);
        }

        private void SetHealth(int amount, out int delta)
        {
            Debug.Assert(IsAlive, "Dead Health can't be changed");
            int prev = Current;
            Current = Mathf.Clamp(amount, 0, Max);
            Changed?.Invoke();

            delta = Current - prev;
            if (delta == 0)
            {

            }
            else
            {
                Changed?.Invoke();

                if (delta > 0)
                {
                    Healed?.Invoke(delta);
                }
                else
                {
                    TookDamage?.Invoke(delta);
                    if(Current == 0)
                    {
                        Die();
                    }
                }
            }
        }
        private void Die()
        {
            IsAlive = false;
            Died?.Invoke();
        }

        public void AddInvulnerable()
        {
            shields++;
            if (shields == 1)
                InvulnerableChanged?.Invoke();
        }
        public void RemoveInvulnerable()
        {
            shields--;
            if (shields == 0)
                InvulnerableChanged?.Invoke();
        }

        public void ResetToMax()
        {
            IsAlive = true;
            Current = Max;
            shields = 0;
        }

        public void ResetTo(int current, int max)
        {
            IsAlive = true;
            shields = 0;
            Max = max;
            Current = current;
            OnReset();
        }

        private void OnReset()
        {
            Changed?.Invoke();
        }
    }
}
