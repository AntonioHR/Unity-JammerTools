using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PointNSheep.Common.TrackedCollections
{
    public class ComponentTrackerBinding : MonoInstaller
    {
        public List<MonoBehaviour> trackables;
        public override void InstallBindings()
        {
            Debug.Assert(trackables.All(t => t.gameObject.scene != null), "Can't track prefabs");

            foreach (var trackable in trackables)
            {
                BindTracker(trackable);
            }
        }

        private void BindTracker(MonoBehaviour trackable)
        {
            var type = typeof(TrackedCollection<>.Tracker);
            Type[] typeArgs = { trackable.GetType() };
            var makeme = type.MakeGenericType(typeArgs);

            Container.BindInterfacesAndSelfTo(type);
        }
    }
}
