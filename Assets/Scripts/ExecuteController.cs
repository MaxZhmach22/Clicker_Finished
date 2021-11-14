using System.Collections.Generic;

namespace MonsterClicker
{
    internal sealed class ExecuteController : IExecute, IDispose
    {
        #region Fields

        private readonly List<IExecute> _executeList;
        private readonly List<IDispose> _disposeList;

        #endregion


        #region ClassLifeCycles

        public ExecuteController()
        { 
            _executeList = new List<IExecute>();
            _disposeList = new List<IDispose>();
        }
           
        #endregion


        #region Methods

        public ExecuteController Add(IController controller)
        {
            if (controller is IExecute execute)
                _executeList.Add(execute);
            return this;
        }

        public ExecuteController Dispose(IController controller)
        {
            if (controller is IDispose dispose)
                _disposeList.Add(dispose);
            return this;
        }
           
        #endregion


        #region IExecute

        public void Execute(float deltaTime)
        {
            for (var index = 0; index < _executeList.Count; ++index)
                _executeList[index].Execute(deltaTime);
        }

        #endregion


        #region IDispose

        public void Dispose()
        {
            for (var index = 0; index < _disposeList.Count; ++index)
                _disposeList[index].Dispose();
        }

        #endregion
    }
}