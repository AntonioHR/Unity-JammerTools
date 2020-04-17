using PointNSheep.Common.Interactables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PointNSheep.Common.CombatBasics
{
    public class AOEElement : ObjectTrigger<IUnit>
    {
        [InjectOptional]
        private IAreaOfEffect owner;


        [Inject]
        private void Init()
        {
            if (owner == null)
                owner = GetComponentInParent<IAreaOfEffect>();
        }
        protected override void OnTriggered(IUnit unit)
        {
            owner.TryHitUnit(unit);
        }
    }
}
