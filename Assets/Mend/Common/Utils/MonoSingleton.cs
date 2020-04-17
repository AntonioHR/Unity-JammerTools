using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PointNSheep.Common.Utils
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                    CreateInstance();
                return instance;
            }
        }

        private static void CreateInstance()
        {
            GameObject obj = new GameObject(string.Format("Mono Singleton - {0}", typeof(T).Name));

            instance = obj.AddComponent<T>();
        }
    }
}
