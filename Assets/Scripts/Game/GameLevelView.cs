using UnityEngine;
using Zenject;

namespace Clicker
{
    public sealed class GameLevelView : MonoBehaviour
    {

        private void Start()
        {
            transform.position = Vector3.zero;
        }

        public sealed class Factory : PlaceholderFactory<GameLevelView>
        {
        }
    }
}