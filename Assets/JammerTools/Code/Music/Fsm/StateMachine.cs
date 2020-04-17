using JammerTools.StateMachines;
using System;

namespace JammerTools.Music
{
    public class StateMachine : StateMachine<MusicManagerInternal, State>
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