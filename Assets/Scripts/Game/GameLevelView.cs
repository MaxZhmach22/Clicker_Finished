using UnityEngine;
using Zenject;

namespace Clicker
{
    public sealed class GameLevelView : MonoBehaviour
    {
        [SerializeField] private GameObject _plane;

        [field: SerializeField] public GameObject UpCenterSpawnPoint { get; private set; }
        [field: SerializeField] public GameObject DownCenterSpawnPoint { get; private set; }
        [field: SerializeField] public GameObject RightCenterSpawnPoint { get; private set; }
        [field: SerializeField] public GameObject LeftCenterSpawnPoint { get; private set; }

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