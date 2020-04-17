using System;

namespace PointNSheep.Common.Music
{
    public abstract class State : StateMachines.State<MusicManagerInternal, State>
    {
        public MusicManagerInternal Music { get { return Context; } }
        public abstract void OnMusicRequestWithStinger(MusicTrack track, MusicTrack stinger);

        public abstract void OnMusicRequest(MusicTrack track);

        public virtual void OnInitializeMusicManager() { }

        protected TrackPlayer SpawnNewPlayer()
        {
            return Music.PlayerPool.Spawn();
        }

    }
}