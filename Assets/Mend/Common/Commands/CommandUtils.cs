using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PointNSheep.Common.Commands
{
    public abstract partial class Command
    {
        //Static Functions
        public static ParallelCommand Parallel(params Command[] commands)
        {
            return new ParallelCommand(commands);
        }
        public static SequenceCommand Sequence(params Command[] commands)
        {
            return new SequenceCommand(commands);
        }
        public static ParallelCommand DelayedSequence(float time, params Command[] commands)
        {
            return new ParallelCommand(commands.Select(c=>c.WithDelay(time)).ToArray());
        }
        public static InstantActionCommand FromInstantAction(Action action)
        {
            return new InstantActionCommand(action);
        }
        public static ActionWithCallbackCommand FromActionWithCallback(Action<Action> action)
        {
            return new ActionWithCallbackCommand(action);
        }
        public static ResultCallbackCommand<T> FromActionWithResultCallback<T>(Action<Action<T>> action, Action<T> onResult)
        {
            return new ResultCallbackCommand<T>(action, onResult);
        }

        //Helpers
        public DelayedCommand WithDelay(float time)
        {
            Debug.Assert(!this.HasStarted);
            return new DelayedCommand(time, this);
        }


        public void DebugOnCreate()
        {
#if UNITY_EDITOR
            CommandDebugger.Instance.DebugRegisterCommand(this);
#endif
        }
        public void DebugRegisterChild(Command child)
        {
#if UNITY_EDITOR
            CommandDebugger.Instance.DebugRegisterParentage(this, child);
#endif
        }
    }
}
