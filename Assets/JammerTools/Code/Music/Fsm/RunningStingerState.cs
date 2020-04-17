using System;

namespace JammerTools.Music
{
    public class RunningStingerState : State
    {
        private MusicTrack loop;
        private MusicTrack stinger;
        private TrackPlayer stingerPlayer;
        private TrackPlayer loopPlayer;

        public RunningStingerState(MusicTrack loop, MusicTrack stinger)
        {
            this.loop = loop;
            this.stinger = stinger;
        }


        protected override void Begin()
        {
            stingerPlayer = SpawnNewPlayer();
            loopPlayer = SpawnNewPlayer();
            stingerPlayer.PlayOnce(stinger, OnStingerFinished);
            loopPlayer.PlayLoopedScheduled(loop, stinger.Duration);
        }

        private void OnStingerFinished()
        {
            ExitTo(new SingleTrackPlayingState(loopPlayer));
        }

        public override void OnMusicRequest(MusicTrack track)
        {
            if (track != this.loop)
            {
                stingerPlayer.FadeOut(Music.DefaultFadeTime);
                loopPlayer.FadeOut(Music.DefaultFadeTime);
                ExitTo(new FadeInState(track));
            }
        }

        public override void OnMusicRequestWithStinger(MusicTrack track, MusicTrack stinger)
        {
            if (track != this.loop)
            {
                stingerPlayer.StopInstant();
                loopPlayer.StopInstant();
                ExitTo(new RunningStingerState(track, stinger));
            }
        }
    }
}