using UnityEngine;
using Zenject;

namespace Clicker
{
    public sealed class GameLevelView : MonoBehaviour
    {
        [SerializeField] private GameObject _plane;
        [SerializeField] private float _speed;

        private void Start()
        {
            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            _plane.transform.Rotate(Vector3.up, _speed * Time.deltaTime);
        }



    }
}