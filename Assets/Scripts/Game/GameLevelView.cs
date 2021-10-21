using UnityEngine;
using Zenject;

namespace Clicker
{
    public sealed class GameLevelView : MonoBehaviour
    {
        [SerializeField] private GameObject _plane;

        private void Start()
        {
            transform.position = Vector3.zero;
        }

        private void Update()
        {
            _plane.transform.Rotate(Vector3.up, Time.deltaTime);
        }

        public sealed class Factory : PlaceholderFactory<GameLevelView>
        {
        }
    }
}