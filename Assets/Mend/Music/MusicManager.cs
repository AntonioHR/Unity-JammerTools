using JammerTools.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PointNSheep.Common.Music
{
    //This is just a facade
    public class MusicManager : MonoSingleton<MusicManager>
    {

        [SerializeField]
        private Transform trackPlayersParent;

        private MusicManagerInternal MusicManagerInternal;

        public Transform TrackPlayersTransform { get => trackPlayersParent; }
        private void Start()
        {
            var settigns = Resources.Load<MusicManagerSettings>(MusicManagerSettings.ResourcesPath);
            //trackPlayersParent = new GameObject("Track Players");
            MusicManagerInternal = new MusicManagerInternal(this, settigns);
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
