using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace PointNSheep.Common.TrackedCollections
{
    public class TrackedCollection<T>
    {
        private List<T> allItems;
        public TrackedCollection():this(new List<T>())
        {
        }
        public TrackedCollection(List<T> startingItems)
        {
            this.allItems = startingItems;
        }

        public IEnumerable<T> AllItems { get => allItems.AsReadOnly(); }

        private void OnNewItem(T item)
        {
            allItems.Add(item);
        }
        private void OnItemDisposed(T item)
        {
            allItems.Remove(item);
        }
        public class Tracker : IInitializable, IDisposable
        {
            [Inject]
            private TrackedCollection<T> collection;
            [Inject]
            T item;
            public void Initialize()
            {
                collection.OnNewItem(item);
            }
            public void Dispose()
            {
                collection.OnItemDisposed(item);
            }

        }
    }

}
