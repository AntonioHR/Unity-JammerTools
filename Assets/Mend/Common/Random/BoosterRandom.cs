using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.Random
{
    public class BoosterRandom<T>
    {

        private class Rarity
        {
            public T[] cards;
            public int tempCount = 0;

            public Rarity(T[] cards)
            {
                this.cards = cards;
            }
        }
        private WeightedRandom<Rarity> pool;


        public BoosterRandom(T[][] cardsByRarity, float[] weights)
        {
            Rarity[] rarities = cardsByRarity.Select(cs => new Rarity(cs)).ToArray();
            pool = WeightedRandom<Rarity>.FromArrays(rarities, weights);
        }

        public IEnumerable<T> GenerateBooster(int count)
        {
            UnityEngine.Debug.Assert(pool.AllItems.All(p=>p.cards.Length >= count), "No rarity should have less cards than booster size");
            while(count > 0)
            {
                pool.GetNew().tempCount++;
                count--;
            }

            var result = pool.AllItems
                .Select(r => r.cards.OrderBy(c => UnityEngine.Random.value).Take(r.tempCount))
                .SelectMany(r=>r).ToArray();

            foreach (var item in pool.AllItems)
            {
                item.tempCount = 0;
            }
            return result;
        }
    }
}
