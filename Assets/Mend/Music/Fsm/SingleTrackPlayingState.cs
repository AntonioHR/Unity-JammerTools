namespace PointNSheep.Common.Music
{
    /// <summary>
    /// This state expects the player to already be playing the track
    /// </summary>
    public class SingleTrackPlayingState : State
    {
        private TrackPlayer p;

        public SingleTrackPlayingState(TrackPlayer p)
        {
            this.p = p;
        }

        public override void OnMusicRequestWithStinger(MusicTrack track, MusicTrack stinger)
        {
            if (track != p.Track)
            {
                p.StopInstant();
                ExitTo(new RunningStingerState(track, stinger));
            }
        }

        public override void OnMusicRequest(MusicTrack track)
        {
            if (track != p.Track)
            {
                p.FadeOut(Music.DefaultFadeTime);
                ExitTo(new FadeInState(track));
            }
        }
    }
}