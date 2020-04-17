using UnityEngine;
using Zenject;

namespace PointNSheep.Common.CombatBasics
{
    public abstract class MonoHealthUnitBase : MonoUnitBase, IUnit
    {
        [SerializeField]
        private Health.Setup setup;

        protected Health health;

        public override IHealth Health => health;


        [Inject]
        private void Initialize()
        {
            InitHealth();
        }
        private void InitHealth()
        {
            health = new Health(setup);
            OnBuiltHealth();
        }

    }
}