using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JammerTools.Commands
{
    public class ResultCallbackCommand<T> : Command
    {
        public event Action<T> ResultArrived;
        private Action<Action<T>> action;
        
        public T Result { get; private set; }

        public ResultCallbackCommand(Action<Action<T>> action, Action<T> resultCallback = null)
        {
            this.action = action;
            if (resultCallback != null)
                ResultArrived += resultCallback;
        }

        protected override void OnRun()
        {
            action(OnResult);
        }

        private void OnResult(T obj)
        {
            this.Result = obj;
            ResultArrived?.Invoke(obj);
            Finish();
        }
    }
}
