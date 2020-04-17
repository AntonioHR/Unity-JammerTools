using System;

namespace PointNSheep.Common.Music
{
    public class StateMachine : StateMachines.StateMachine<MusicManagerInternal, State>
    {
        public override State DefaultState => new NoMusicState();

        public void OnMusicRequestWithStinger(MusicTrack track, MusicTrack stinger)
        {
            CurrentState.OnMusicRequestWithStinger(track, stinger);
        }

        public void OnMusicRequest(MusicTrack track)
        {
            CurrentState.OnMusicRequest(track);
        }

        public void OnInitializeMusicManager()
        {
            CurrentState.OnInitializeMusicManager();
        }
    }
}