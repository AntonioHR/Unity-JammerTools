using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.Random
{
    public class WeightedRandom<T>
    {
        private class Entry
        {
            public float factor;
            public T value;

            public Entry(float factor, T value)
            {
                this.factor = factor;
                this.value = value;
            }
        }

        Entry[] entries;

        public WeightedRandom(T[] values, float[] factors)
        {
            UnityEngine.Debug.Assert(values.Length == factors.Length);
            GenerateEntries(values, factors);
        }

        void GenerateEntries(T[] values, float[] weights)
        {
            float weightFactor = weights.Sum();
            weights = weights.Select(w => w / weightFactor).ToArray();
            entries = new Entry[values.Length];

            float sumUpToNow = 0;
            for (int i = 0; i < entries.Length; i++)
            {
                sumUpToNow += weights[i];
                entries[i] = new Entry(sumUpToNow, values[i]);
                if (i == entries.Length - 1)
                    entries[i].factor = float.PositiveInfinity;
            }
        }

        public IEnumerable<T> AllItems
        {
            get
            {
                return entries.Select(e => e.value);
            }
        }
        public T GetNew()
        {
            float random = UnityEngine.Random.value;
            return entries.First(e => e.factor > random).value;
        }

        public static WeightedRandom<T> FromGetters<T1>(T1[] vals, Func<T1, T> valueGetter, Func<T1, float> factorGetter)
        {
            return new WeightedRandom<T>(
                vals.Select(v => valueGetter(v)).ToArray(), 
                vals.Select(v => factorGetter(v)).ToArray());
        }

        public static WeightedRandom<T> FromGetter(T[] vals, Func<T, float> factorGetter)
        {
            return new WeightedRandom<T>(
                vals,
                vals.Select(v => factorGetter(v)).ToArray());
        }

        public static WeightedRandom<T> FromArrays(T[] values, float[] factors)
        {
            return new WeightedRandom<T>(values, factors);
        }
        public static WeightedRandom<T> FromGroupArrays(T[][] values, float[] factors)
        {
            var factorTuples = values.
                SelectMany((vals, i) => 
                                vals.Select(v => (v, factors[i]).ToTuple()));
            return FromTuples(factorTuples.ToArray());
        }
        public static WeightedRandom<T> FromTuples(Tuple<T, float>[] values)
        {
            return FromGetters(values, v=>v.Item1, v=>v.Item2);
        }
    }
}
