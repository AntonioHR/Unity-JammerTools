using JammerTools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JammerTools.Music
{
    [AddComponentMenu("JammerMusic/Music Change Trigger")]
    public class MusicChangeTrigger : MonoTrigger
    {
        [SerializeField]
        private MusicTrack track;
        [SerializeField]
        private bool useStinger;
        [SerializeField]
        private MusicTrack stinger;
        protected override void OnTriggered()
        {
            if (useStinger)
                MusicManager.Instance.SetMusic(track, stinger);
            else
                MusicManager.Instance.SetMusic(track);
        }
    }
}
