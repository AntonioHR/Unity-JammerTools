using PointNSheep.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PointNSheep.Common.Timers
{
    public class MonoWaitHandler : MonoSingleton<MonoWaitHandler>
    {
        public void WaitSecondsThenDo(float time, Action callback)
        {
            StartCoroutine(CoroutineUtils.WaitThenDo(time, callback));
        }
    }
    public static class Wait
    {
        public static void ForSecondsThenDo(float time, Action callback)
        {
            MonoWaitHandler.Instance.WaitSecondsThenDo(time, callback);
        }
    }
}
