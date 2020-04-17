using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace PointNSheep.Common.CombatBasics
{
    public class HealthText : MonoBehaviour
    {
        [SerializeField]
        private string format = "{0}/{1}";
        [SerializeField]
        private Text text;
        private IHealth health;

        private void Start()
        {
            if (text == null)
                text = GetComponent<Text>();

            Debug.Assert(text != null);

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
            text.text = string.Format(format, health.Current.ToString("D"), health.Max.ToString("D"));
        }
    }
}
