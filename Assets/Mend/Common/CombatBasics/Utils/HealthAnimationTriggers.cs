using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PointNSheep.Common.CombatBasics
{

    public class HealthAnimationTriggers : MonoBehaviour
    {
        private IUnit unit;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private string DamageTrigger = "OnDamage";
        [SerializeField]
        private string HealTrigger = "OnHeal";
        [SerializeField]
        private string DeathTrigger = "OnDeath";
        [SerializeField]
        private string IntHealthVar = "Health";
        private void Start()
        {

            if (animator == null)
                animator = GetComponent<Animator>();

            unit = GetComponentInParent<IUnit>();
            unit.Health.TookDamage += Health_TookDamage;
            unit.Health.Healed += Health_Healed;
            unit.Health.Died += Health_Died;
            unit.Health.Changed += Health_Changed;
            Health_Changed();
        }

        private void Health_Changed()
        {
            animator.SetInteger(IntHealthVar, unit.Health.Current);
        }

        private void Health_Died()
        {
            animator.SetTrigger(DeathTrigger);
        }
        private void Health_Healed(int delta)
        {
            animator.SetTrigger(HealTrigger);
        }
        private void Health_TookDamage(int delta)
        {
            animator.SetTrigger(DamageTrigger);
        }
    }
}
