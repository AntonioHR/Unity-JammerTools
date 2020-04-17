using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JammerTools.Common
{
    public class MonoWaitHandler : MonoSingleton<MonoWaitHandler>
    {
        public void WaitSecondsThenDo(float time, Action callback)
        {
            StartCoroutine(WaitThenDoCoroutine(time, callback));
        }

        private static IEnumerator WaitThenDoCoroutine(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action();
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
