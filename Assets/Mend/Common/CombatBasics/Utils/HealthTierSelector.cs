using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PointNSheep.Common.CombatBasics
{
    public class HealthTierSelector : MonoBehaviour
    {
        private IUnit unit;

        private List<GameObject> children;
        [SerializeField]
        private float[] thresholds;

        private int currTier;
        private void Start()
        {
            unit = GetComponentInParent<IUnit>();
            unit.Health.Changed += Health_Changed;

            InitChildren();
            InitTiers();
        }

        private void InitTiers()
        {


            currTier = TierFor(unit.Health.Current);


            for (int i = 0; i < children.Count; i++)
            {
                children[i].gameObject.SetActive(i == currTier);
            }
        }
        private void InitChildren()
        {
            children = new List<GameObject>();

            foreach (Transform child in transform)
            {
                children.Add(child.gameObject);
            }
            Debug.Assert(children.Count == thresholds.Length + 1, "You need exactly one less threshold than children");
        }
        private void Health_Changed()
        {
            int nextTier = TierFor(unit.Health.Current);
            if(nextTier != currTier)
            {
                children[currTier].gameObject.SetActive(false);
                children[nextTier].gameObject.SetActive(true);
                currTier = nextTier;
            }
        }
        private int TierFor(float current)
        {
            int i = 0;
            for (; i < thresholds.Length; i++)
            {
                if (thresholds[i] > current)
                    break;
            }
            return i;
        }

    }
}
