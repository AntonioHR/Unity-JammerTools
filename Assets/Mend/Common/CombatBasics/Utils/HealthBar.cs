using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace PointNSheep.Common.CombatBasics
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image fill;
        private IHealth health;

        private void Start()
        {
            if (fill == null)
                fill = GetComponent<Image>();

            Debug.Assert(fill != null);

            var unit = GetComponentInParent<IUnit>();
            Init(unit.Health);
        }

        private void Init(IHealth health)
        {
            this.health = health;
            health.Changed += UpdateValue;
            UpdateValue();
        }

        private void UpdateValue()
        {
            fill.fillAmount = health.Percent();
        }
    }
}
