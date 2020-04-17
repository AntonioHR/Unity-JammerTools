using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PointNSheep.Common.Commands
{
    public delegate void CommandAction(Command c);

    public abstract partial class Command
    {
        public event CommandAction StartedRunning;
        public event CommandAction Finished;
        public enum State { Idle, Running, Finished }



        public State CurrentState { get; private set; } = State.Idle;
        public bool HasFinished { get => CurrentState == State.Finished; }
        public bool IsRunning { get => CurrentState == State.Running; }
        public bool HasStarted { get => CurrentState != State.Idle; }

        protected Command()
        {
            DebugOnCreate();
        }

        public void Run(CommandAction callback)
        {
            Finished += callback;
            Run();
        }
        public void Run(Action callback)
        {
            Finished += (c) => callback();
            Run();
        }

        private void Run()
        {
            OnRun();
            StartedRunning?.Invoke(this);
        }

        protected void Finish()
        {
            Debug.Assert(!HasFinished);

            CurrentState = State.Finished;
            Finished.Invoke(this);
        }
        protected abstract void OnRun();

    }
}
