using JammerTools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JammerTools.Music
{
    //This is just a facade
    public class MusicManager : MonoSingleton<MusicManager>
    {

        [SerializeField]
        private Transform trackPlayersParent;

        private MusicManagerInternal MusicManagerInternal;

        public Transform TrackPlayersTransform { get => trackPlayersParent; }

        protected override bool KeepBetweenScenes => true;

        protected override void Init()
        {
            var settings = Resources.Load<MusicManagerSettings>(MusicManagerSettings.ResourcesPath);
            Debug.Assert(settings != null);
            MusicManagerInternal = new MusicManagerInternal(this, settings);
            MusicManagerInternal.Initialize();
        }

        public void SetMusic(MusicTrack track, MusicTrack stinger)
        {
            MusicManagerInternal.SetMusic(track, stinger);
        }
        public void SetMusic(MusicTrack track)
        {
            MusicManagerInternal.SetMusic(track);
        }

    }
}
