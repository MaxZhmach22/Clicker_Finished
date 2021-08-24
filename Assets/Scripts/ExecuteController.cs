using System.Collections.Generic;

namespace MonsterClicker
{
    internal sealed class ExecuteController : IExecute, ILateExecute
    {
        private readonly List<IExecute> _executeList;
        private readonly List<ILateExecute> _lateExecuteList;

        public ExecuteController()
        {
            _executeList = new List<IExecute>();
            _lateExecuteList = new List<ILateExecute>();
        }

        public ExecuteController Add(IController controller)
        {
            if(controller is IExecute execute)
            {
                _executeList.Add(execute);
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecuteList.Add(lateExecute);
            }

            return this;
        }

        public void Execute(float deltaTime)
        {
           for(var index = 0; index<_executeList.Count; ++index)
            {
                _executeList[index].Execute(deltaTime);
            }
        }

        public void LateExecute(float deltaTime)
        {
            for (var index = 0; index < _lateExecuteList.Count; ++index)
            {
                _lateExecuteList[index].LateExecute(deltaTime);
            }
        }
    }
}