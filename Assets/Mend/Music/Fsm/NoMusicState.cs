namespace PointNSheep.Common.Music
{
    public class NoMusicState : State
    {
        public override void OnMusicRequestWithStinger(MusicTrack track, MusicTrack stinger)
        {
            ExitTo(new RunningStingerState(track, stinger));
        }

        public override void OnMusicRequest(MusicTrack track)
        {
            TrackPlayer p = SpawnNewPlayer();
            p.PlayLooped(track);
            ExitTo(new SingleTrackPlayingState(p));
        }

        public override void OnInitializeMusicManager()
        {
            var settings = Music.settings;
            if (settings.startingTrack != null)
            {
                if (settings.startStinger != null)
                {
                    OnMusicRequestWithStinger(settings.startingTrack, settings.startStinger);
                }
                else
                {
                    OnMusicRequest(settings.startingTrack);
                }
            }
        }
    }
}