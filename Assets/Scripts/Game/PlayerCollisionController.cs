using Zenject;
using UniRx;
using System;
using UnityEngine;

namespace Clicker
{
    internal sealed class PlayerCollisionController : BaseController
    {
        private readonly Player _player;
        
        private int _counts = 0;
        private CompositeDisposable _disposables = new CompositeDisposable();

        public PlayerCollisionController(
            Player player)
        {
            _player = player;
        }

        public override void Start()
        {
            _player.CollisionGameObject.Subscribe(_ => 
            {
                _counts++;
                CollisionCountCheker();
                }).AddTo(_disposables);
            Debug.Log($"{nameof(PlayerCollisionController)} Is Subcribed; Disposables count = {_disposables.Count}");
        }

        public override void Dispose()
        {
            _disposables.Clear();
            Debug.Log($"{nameof(PlayerCollisionController)} Is Disposed; Disposables count = {_disposables.Count}");
        }

        private void CollisionCountCheker()
        {
            //if (_counts >= _player.LifeCounts)
            //    _player.ChangeState(GameStates.Start);
  
        }
    }
}
