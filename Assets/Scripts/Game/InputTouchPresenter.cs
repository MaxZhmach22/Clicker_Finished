using UnityEngine;
using UniRx;
using System;

namespace Clicker
{
    internal sealed class InputTouchPresenter : MonoBehaviour
    {
        public ReactiveProperty<IEnemy> Enemy { get; private set; } = new ReactiveProperty<IEnemy>();

        private void Start()
        {
            var touchStream = Observable.EveryUpdate().Where(_ => Input.touchCount > 0).Select(x => Input.GetTouch(0));
            touchStream.Subscribe(x =>
            {
                Touch touch = x;
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        HitEnemy(touch.position);
                        break;
                }
            });
        }

        private void HitEnemy(Vector2 position)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(position.x, position.y, 0));
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.gameObject.TryGetComponent<IEnemy>(out var enemy))
                {
                    Debug.Log("1");
                    Enemy.Value = enemy;
                    Enemy.Value = null;
                }
            }
        }
    }
}
