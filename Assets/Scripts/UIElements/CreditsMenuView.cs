using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Clicker
{
    public sealed class CreditsMenuView : MonoBehaviour
    {
        [field: SerializeField] public Button BackBtn { get; private set; }

        public sealed class Factory : PlaceholderFactory<CreditsMenuView>
        {
        }
    }
}