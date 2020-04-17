using JammerTools.StateMachines;
using System;

namespace PointNSheep.Common.Music
{
    public abstract class State : State<MusicManagerInternal, State>
    {
        public MusicManagerInternal Music { get { return Context; } }
        public abstract void OnMusicRequestWithStinger(MusicTrack track, MusicTrack stinger);

        public abstract void OnMusicRequest(MusicTrack track);

        public virtual void OnInitializeMusicManager() { }

        protected TrackPlayer SpawnNewPlayer()
        {
            return PoolManager.SpawnObject(Context.settings.trackPlayerPrefab.gameObject)
                .GetComponent<TrackPlayer>();
        }

    }
}