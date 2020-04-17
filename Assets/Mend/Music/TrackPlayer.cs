using JammerTools.Common;
using System;
using System.Collections;
using UnityEngine;

namespace PointNSheep.Common.Music
{
    public class TrackPlayer : MonoBehaviour
    {
        //Inspector Dependencies
        [SerializeField]
        private AudioSource audioSource;
        //Injected Dependencies

        //State
        private MusicTrack track;
        private Coroutine fadeCoroutine;
        private const float DespawnExtraTime = 0.1f;

        int playID = 0;

        public MusicTrack Track { get => track; }

        public void FadeOut(float time)
        {
            int id = playID;
            KillFadeCoroutineIfAny();
            fadeCoroutine = StartCoroutine(SoundFadeCoroutine(0, time, ()=>DespawnWithCheck(id)));
        }
        public void StopInstant()
        {
            audioSource.Stop();
            Despawn();
        }
        public void FadeIn(float time, Action callback)
        {
            audioSource.volume = 0;
            KillFadeCoroutineIfAny();
            fadeCoroutine = StartCoroutine(SoundFadeCoroutine(track.Volume, time, callback));
        }

        public void PlayOnce(MusicTrack musicTrack, Action callback)
        {
            SetTrack(musicTrack, false);
            audioSource.Play();
            int id = playID;
            Wait.ForSecondsThenDo(musicTrack.Clip.length + DespawnExtraTime, ()=>
            {
                DespawnWithCheck(id);
                callback();
                });
        }
        public void PlayLooped(MusicTrack musicTrack)
        {
            SetTrack(musicTrack);
            audioSource.Play();
        }
        public void PlayLoopedScheduled(MusicTrack musicTrack, float delay)
        {
            SetTrack(musicTrack);
            audioSource.PlayScheduled(AudioSettings.dspTime + delay);
        }
        private void SetTrack(MusicTrack musicTrack, bool looped = true)
        {
            playID++;
            this.track = musicTrack;
            audioSource.loop = looped;
            audioSource.clip = musicTrack.Clip;
            audioSource.volume = musicTrack.Volume;
            audioSource.outputAudioMixerGroup = musicTrack.MixerGroup;
        }


        private void DespawnWithCheck(int intendedPlayID)
        {
            if(intendedPlayID == playID)
            {
                Despawn();
            }
        }
        private void Despawn()
        {
            KillFadeCoroutineIfAny();
            PoolManager.ReleaseObject(this.gameObject);
        }


        private void KillFadeCoroutineIfAny()
        {
            if(fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
        }

        private IEnumerator SoundFadeCoroutine(float target, float duration, Action callback)
        {
            float start = audioSource.volume;
            Timer t = Timer.CreateAndStart();
            while (t.ElapsedSeconds < duration)
            {
                audioSource.volume = Mathf.Lerp(start, target, t.ElapsedSeconds / duration);

                yield return null;
            }

            audioSource.volume = target;

            callback?.Invoke();
        }
    }
}