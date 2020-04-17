using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace PointNSheep.Common.CombatBasics
{

    public class HealthSpriteColors : MonoBehaviour
    {
        [Serializable]
        public class BlinkStyle
        {
            public bool enabled = false;
            public Color color = Color.white;
            public Ease ease = Ease.Linear;
            public float duration = 1f;
            public int flashCount = 5;
            public bool from;

        }
        private IUnit unit;
        [SerializeField]
        private SpriteRenderer sprite;
        [SerializeField]
        private BlinkStyle Damage;
        [SerializeField]
        private BlinkStyle Heal;
        [SerializeField]
        private BlinkStyle Death;
        private TweenerCore<Color, Color, ColorOptions> tween;

        private void Start()
        {
            unit = GetComponentInParent<IUnit>();
            unit.Health.TookDamage += Health_TookDamage;
            unit.Health.Healed += Health_Healed;
            unit.Health.Died += Health_Died;

            if (sprite == null)
                sprite = GetComponent<SpriteRenderer>();
        }

        private void Health_Died()
        {
            Blink(Death);
        }
        private void Health_Healed(int delta)
        {
            Blink(Heal);
        }
        private void Health_TookDamage(int delta)
        {
            Blink(Damage);
        }

        private void Blink(BlinkStyle style)
        {
            if (tween != null)
                tween.Complete();

            if (!style.enabled)
                return;
            tween = sprite.DOColor(style.color, style.duration)
                .SetEase(style.ease, style.flashCount, 0);
            if (style.from)
                tween.From();
        }
    }
}
