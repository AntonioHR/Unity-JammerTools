using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PointNSheep.Common.Utils
{
    public static class LinqExtensions
    {
        public static T WithMinValue<T>(this IEnumerable<T> collection, Func<T, float> func, bool defaultIfEmpty = true)
        {
            return collection.WithMaxValue(func, true, defaultIfEmpty);
        }
        public static T WithMaxValue<T>(this IEnumerable<T> collection, Func<T, float> func, bool defaultIfEmpty = true)
        {
            return collection.WithMaxValue(func, false, defaultIfEmpty);
        }

        private static T WithMaxValue<T>(this IEnumerable<T> collection, Func<T, float> func, bool invert, bool defaultIfEmpty = true)
        {
            Debug.Assert(collection.Any() || defaultIfEmpty);
            if (!collection.Any() && defaultIfEmpty)
                return default(T);

            //Initialize with First Item
            float maxVal = func(collection.First());
            T maxItem = collection.First();

            foreach (T item in collection.Skip(1))
            {
                float val = func(item);
                if(invert)
                {
                    if (val < maxVal)
                    {
                        val = maxVal;
                        maxItem = item;
                    }
                } else
                {
                    if (val > maxVal)
                    {
                        val = maxVal;
                        maxItem = item;
                    }
                }
            }
            return maxItem;
        }



        #region Distance Helpers
        public static Vector3 GetClosestTo(this IEnumerable<Vector3> collection, Vector3 target)
        {
            return collection.WithMinValue((e) => Vector3.Distance(e, target));
        }
        public static Vector3 GetFurthestFrom(this IEnumerable<Vector3> collection, Vector3 target)
        {
            return collection.WithMaxValue((e) => Vector3.Distance(e, target));
        }

        public static Transform GetClosestTo(this IEnumerable<Transform> collection, Transform target)
        {
            return collection.WithMinValue((e) => Vector3.Distance(e.position, target.position));
        }
        public static Transform GetFurthestFrom(this IEnumerable<Transform> collection, Transform target)
        {
            return collection.WithMaxValue((e) => Vector3.Distance(e.position, target.position));
        }

        public static Transform GetClosestTo(this IEnumerable<Transform> collection, Vector3 point)
        {
            return collection.WithMinValue((e) => Vector3.Distance(e.position, point));
        }
        public static Transform GetFurthestFrom(this IEnumerable<Transform> collection, Vector3 point)
        {
            return collection.WithMaxValue((e) => Vector3.Distance(e.position, point));
        }
        #endregion

    }
}
