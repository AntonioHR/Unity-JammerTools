using UnityEngine;
using UnityEngine.Events;

namespace PointNSheep.Common.CombatBasics
{
    public abstract class MonoUnitBase : MonoBehaviour, IUnit
    {
        [SerializeField]
        protected UnityEvent Died;
        [SerializeField]
        protected UnityEvent TookDamage;
        [SerializeField]
        protected UnityEvent Healed;

        public abstract IHealth Health { get; }
        public abstract ITeam Team { get; }
        Transform IUnit.Transform => transform;



        protected void OnBuiltHealth()
        {
            Health.Died += Health_Died;
            Health.TookDamage += Health_TookDamage;
            Health.Healed += Health_Healed;
        }

        #region Event Propagation
        private void Health_Died()
        {
            Died.Invoke();
        }
        private void Health_TookDamage(int delta)
        {
            TookDamage.Invoke();
        }
        private void Health_Healed(int delta)
        {
            Healed.Invoke();
        }

        #endregion
    }
}