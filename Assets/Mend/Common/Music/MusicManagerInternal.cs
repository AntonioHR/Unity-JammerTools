using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace PointNSheep.Common.Music
{
    public class MusicManagerInternal
    {
        [Serializable]
        public class Settings
        {
            public float DefaultFadeInTime = .5f;
            public MusicTrack startingTrack;
            public MusicTrack startStinger;
        }
        public MusicManagerInternal(MusicManager facade, Settings settings, TrackPlayer.Pool playerPool)
        {
            Facade = facade;
            PlayerPool = playerPool;
            this.settings = settings;
            StateMachine = new StateMachine();
            StateMachine.Begin(this);
        }


        public void Initialize()
        {
            StateMachine.OnInitializeMusicManager();
        }

        public Settings settings { get; private set; }
        public MusicManager Facade { get; private set; }
        public StateMachine StateMachine { get; private set; }
        public TrackPlayer.Pool PlayerPool { get; private set; }
        public float DefaultFadeTime { get => settings.DefaultFadeInTime; }


        public void SetMusic(MusicTrack track, MusicTrack stinger)
        {
            StateMachine.OnMusicRequestWithStinger(track, stinger);
        }
        public void SetMusic(MusicTrack track)
        {
            StateMachine.OnMusicRequest(track);
        }
    }
}
