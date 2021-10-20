using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Clicker
{
    public sealed class MainMenuView : MonoBehaviour
    {
        [field: SerializeField] public Button CreditsBtn { get; private set; }
        [field: SerializeField] public Button StartGameBtn { get; private set; }


        public sealed class Factory : PlaceholderFactory<MainMenuView>
        {
        }

    }
}
