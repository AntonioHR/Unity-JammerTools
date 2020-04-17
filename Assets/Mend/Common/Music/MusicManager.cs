using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PointNSheep.Common.Music
{
    //This is just a facade
    public class MusicManager : MonoBehaviour
    {

        [SerializeField]
        private Transform trackPlayersParent;

        [Inject]
        private MusicManagerInternal MusicManagerInternal;

        public Transform TrackPlayersTransform { get => trackPlayersParent; }
        private void Start()
        {
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
