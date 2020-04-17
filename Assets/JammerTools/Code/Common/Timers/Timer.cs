using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JammerTools.Common
{
    public class Timer
    {
        private float _startTime;

        public Timer() { }
        bool started = false;

        public float ElapsedSeconds { get { return started? Time.time - _startTime : 0; } }

        public void Restart()
        {
            started = true;
            _startTime = Time.time;
        }

        public void ClearAndStop()
        {
            started = false;
        }




        public static Timer CreateAndStart()
        {
            var result = new Timer();
            result.Restart();
            return result;
        }
    }
}
