using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace PointNSheep.Common.TrackedCollections
{
    public class TrackingInstaller<T> : Installer<TrackingInstaller<T>>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TrackedCollection<T>.Tracker>();
        }
    }

}
