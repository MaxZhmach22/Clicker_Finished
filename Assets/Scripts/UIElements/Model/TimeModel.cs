using System;
using UniRx;
using UnityEngine;
using Zenject;


namespace MonsterClicker
{
    internal sealed class TimeModel : ITimeModel, ITickable
    {
        #region ITimeModel

        public IObservable<int> GameTime => _gameTime.Select(f => (int)f); 

        #endregion


        #region Fields

        private ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();
        private IDisposable _cancel;

        #endregion


        #region Methods

        public void Tick() =>
             _gameTime.Value += Time.deltaTime;
      
        #endregion
    }
}
