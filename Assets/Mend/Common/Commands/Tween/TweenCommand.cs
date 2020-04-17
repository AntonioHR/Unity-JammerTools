using DG.Tweening;
using PointNSheep.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Common.Commands
{
    public class TweenCommand : Command
    {
        private Func<Tween> tweenBuildFunction;

        public TweenCommand(Func<Tween> tweenBuildFunction)
        {
            this.tweenBuildFunction = tweenBuildFunction;
        }

        protected override void OnRun()
        {
            tweenBuildFunction().OnComplete(Finish);
        }
    }

    public partial class Command
    {

        public static TweenCommand FromTweenFunction(Func<Tween> tweenFunc)
        {
            return new TweenCommand(tweenFunc);
        }
    }
}
