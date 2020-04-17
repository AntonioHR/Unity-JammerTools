using UnityEngine;
using UnityEngine.Audio;

namespace JammerTools.Music
{
    [CreateAssetMenu(menuName ="JammerTools/Music Track")]
    public class MusicTrack : ScriptableObject
    {
        [SerializeField]
        private float volume = 1.0f;
        [SerializeField]
        private AudioClip clip;
        [SerializeField]
        private AudioMixerGroup mixerGroup;

        public float Volume { get => volume; }
        public AudioClip Clip { get => clip; }
        public float Duration { get => Clip.length; }
        public AudioMixerGroup MixerGroup { get => mixerGroup; }
    }
}