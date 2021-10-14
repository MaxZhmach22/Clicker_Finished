using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuView : MonoBehaviour
{
    [field: Header("Buttons")]
    [field: SerializeField] public Button _buttonStart { get; private set; }
    [field: SerializeField] public Button _buttonQuit { get; private set; }
    [field: SerializeField] public Button _buttonSettings { get; private set; }

    [Inject]
    public void Init()
    {
        Debug.Log("AF");
    }

    public class Factory : PlaceholderFactory<MainMenuView>
    {
    }

}
