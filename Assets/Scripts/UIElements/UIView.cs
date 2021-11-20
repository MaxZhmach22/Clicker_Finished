using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace MonsterClicker
{
    internal sealed class UiView : MonoBehaviour
    {
        [field: SerializeField] public Button Quit { get; private set; }
        [field: SerializeField] public Button ScoreMenuBtn { get; private set; }
        [field: SerializeField] public Button CreditsMenuBtn { get; private set; }
        [field: SerializeField] public Button StartGame { get; private set; }
        [field: SerializeField] public Button BackScoreBtn { get; private set; }
        [field: SerializeField] public Button BackCreditsBtn { get; private set; }
        [field: SerializeField] public Button PauseBtn { get; private set; }
        [field: SerializeField] public Button MuteBtn { get; private set; }
        [field: SerializeField] public Button BackLooseMenuBtn { get; private set; }
        [field: SerializeField] public Button ClearScoreBtn { get; private set; }
        [field: SerializeField] public TMP_Text TextScoreList { get; private set; }
        [field: SerializeField] public TMP_Text CurrentScoreLooseMenuTxt { get; private set; }
        [field: SerializeField] public TMP_Text CurrentScoreInGameTXT { get; private set; }

    }
}
