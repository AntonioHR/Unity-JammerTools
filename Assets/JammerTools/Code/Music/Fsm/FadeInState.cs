using System;

namespace JammerTools.Music
{
    public class FadeInState : State
    {
        private MusicTrack track;
        private TrackPlayer player;

        public FadeInState(MusicTrack track)
        {
            this.track = track;
        }

        protected override void Begin()
        {
            player = SpawnNewPlayer();
            player.PlayLooped(track);
            player.FadeIn(Music.DefaultFadeTime, OnFadeInOver);
        }

        private void OnFadeInOver()
        {
            ExitTo(new SingleTrackPlayingState(player));
        }

        public override void OnMusicRequestWithStinger(MusicTrack track, MusicTrack stinger)
        {
            if (track != this.track)
            {
                player.StopInstant();
                ExitTo(new RunningStingerState(track, stinger));
            }
        }
        public override void OnMusicRequest(MusicTrack track)
        {
            if(track != this.track)
            {
                player.FadeOut(Music.DefaultFadeTime);
                ExitTo(new FadeInState(track));
            }
        }
    }
}