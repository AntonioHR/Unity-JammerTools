using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PointNSheep.Common.Music
{
    public class MusicManagerInstaller : MonoInstaller
    {
        [SerializeField]
        private MusicManager musicManagerPrefab;
        [SerializeField]
        private TrackPlayer trackPlayerPrefab;
        [SerializeField]
        private MusicManagerInternal.Settings settings;
        public override void InstallBindings()
        {
            Container.Bind<MusicManager>().FromSubContainerResolve()
                .ByMethod(InstallMusicManagerInternalStuff)
                .WithKernel().AsSingle();

        }

        private void InstallMusicManagerInternalStuff(DiContainer c)
        {
            c.Bind<MusicManager>()
                .FromComponentInNewPrefab(musicManagerPrefab)
                .UnderTransform(transform)
                .AsSingle().NonLazy();

            c.BindInterfacesAndSelfTo<MusicManagerInternal>().AsSingle();
            c.Bind<MusicManagerInternal.Settings>().FromInstance(settings);

            c.BindMemoryPool<TrackPlayer, TrackPlayer.Pool>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(trackPlayerPrefab)
                .UnderTransform(context => context.Container.Resolve<MusicManager>().TrackPlayersTransform);
        }

    }
}
