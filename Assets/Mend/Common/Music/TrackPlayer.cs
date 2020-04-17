using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using PointNSheep.Common.Timers;
using System;
using UnityEngine;
using Zenject;

namespace PointNSheep.Common.Music
{
    public class TrackPlayer : MonoBehaviour
    {
        //Inspector Dependencies
        [SerializeField]
        private AudioSource audioSource;
        //Injected Dependencies
        [Inject]
        private Pool pool;

        //State
        private MusicTrack track;
        private TweenerCore<float, float, FloatOptions> audioTween;
        private const float DespawnExtraTime = 0.1f;

        int playID = 0;

        public MusicTrack Track { get => track; }

        public void FadeOut(float time)
        {
            int id = playID;
            KillAudioTweenIfAny();
            audioSource.DOFade(0, time).OnComplete(()=>DespawnWithCheck(id));
        }
        public void StopInstant()
        {
            audioSource.Stop();
            Despawn();
        }
        public void FadeIn(float time, TweenCallback callback)
        {
            audioSource.volume = 0;
            KillAudioTweenIfAny();
            audioTween = audioSource.DOFade(track.Volume, time).OnComplete(callback);
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
            KillAudioTweenIfAny();
            pool.Despawn(this);
        }

        private void KillAudioTweenIfAny()
        {
            if (audioTween != null && audioTween.IsActive())
                audioTween.Kill();
        }

        public class Pool : MonoMemoryPool<TrackPlayer>
        {

        }
    }
}