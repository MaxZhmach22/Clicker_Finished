using UnityEngine;
using UniRx;
using System;
using UnityEngine.EventSystems;
using Zenject;

namespace Clicker
{
    internal sealed class InputTouchPresenter : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;

        private Player _player;
        [Inject]
        private void Init(Player player)
        {
            _player = player;
        }

        public Subject<IEnemy> Enemy { get; private set; } = new Subject<IEnemy>();
        public Subject<Vector3> TouchPosition { get; private set; } = new Subject<Vector3>();

        private void Start()
        {
            var touchStream = Observable.EveryUpdate().Where(_ => Input.touchCount > 0).Select(x => Input.GetTouch(0));
            touchStream.Subscribe(x =>
            {
                if (!_eventSystem.IsPointerOverGameObject())
                {
                    Touch touch = x;
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            HitEnemy(touch.position);
                            break;
                    }
                }
            });
        }

        private void HitEnemy(Vector2 position)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(position.x, position.y));
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.gameObject.TryGetComponent<IEnemy>(out var enemy))
                {
                    Enemy.OnNext(enemy);
                    Debug.Log("enemy");
                }
                else
                {
                    Enemy.OnNext(enemy);
                    TouchPosition.OnNext(hit.point);
                }
                    

                
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                _player.ChangeState(GameStates.Start);
            }
        }
    }
}
