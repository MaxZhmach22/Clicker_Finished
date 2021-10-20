using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Clicker
{
    public sealed class GameUiView : MonoBehaviour
    {
        //[field: SerializeField] public Button CreditsBtn { get; private set; }


        public sealed class Factory : PlaceholderFactory<GameUiView>
        {
        }
    }
}