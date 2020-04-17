using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JammerTools.Common.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class ObjectCheckArea<T> : MonoBehaviour
    {
        public event Action<T> ObjectEntered;
        public event Action<T> ObjectLeft;

        private Dictionary<T, List<Collider2D>> hits = new Dictionary<T, List<Collider2D>>();

        private HashSet<T> objectsInArea = new HashSet<T>();

        public IEnumerable<T> ObjectsInArea { get { return objectsInArea.AsEnumerable(); } }

        public virtual bool IsValid(T obj) { return true; }

        private void Awake()
        {
            Debug.Assert(gameObject.GetComponentsInChildren<Collider2D>().Any(x=>x.isTrigger));
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            T target = TryGetTargetFrom(col);

            if (target != null && IsValid(target))
            {
                CheckHasEntry(target);
                hits[target].Add(col);

                objectsInArea.Add(target);
                OnEnter(target);
                ObjectEntered?.Invoke(target);
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            T target = TryGetTargetFrom(col);

            if (target != null)
            {
                CheckHasEntry(target);
                if (hits[target].Contains(col))
                {
                    hits[target].Remove(col);
                    
                    objectsInArea.Remove(target);
                    OnLeave(target);
                    ObjectLeft?.Invoke(target);
                }
            }
        }

        private static T TryGetTargetFrom(Collider2D col)
        {
            var target = col.GetComponent<T>();

            if (target == null)
            {
                var proxy = col.GetComponent<IProxyFor<T>>();
                if (proxy != null)
                    target = proxy.Owner;
            }

            return target;
        }

        private void CheckHasEntry(T target)
        {
            if (!hits.ContainsKey(target))
                hits.Add(target, new List<Collider2D>());
        }

        protected virtual void OnEnter(T obj) { }
        protected virtual void OnLeave(T obj) { }

    }
}
