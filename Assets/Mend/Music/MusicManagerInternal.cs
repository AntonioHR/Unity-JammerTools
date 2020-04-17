using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.Music
{
    public class MusicManagerInternal
    {
        public MusicManagerInternal(MusicManager facade, MusicManagerSettings settings)
        {
            Facade = facade;
            this.settings = settings;
            StateMachine = new StateMachine();
            StateMachine.Begin(this);
        }


        public void Initialize()
        {
            StateMachine.OnInitializeMusicManager();
        }

        public MusicManagerSettings settings { get; private set; }
        public MusicManager Facade { get; private set; }
        public StateMachine StateMachine { get; private set; }
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
