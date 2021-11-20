using UnityEngine;
using UniRx;
using UnityEngine.UI;


namespace MonsterClicker
{
    internal sealed class MainMenuPresenter : BasePresenter
    {
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _scoreMenu;
        [SerializeField] private GameObject _creditsMenu;

        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _scoreButton;
        [SerializeField] private Button _creditsBtn;
        [SerializeField] private Button _startButton;

        private UiMainMenuModel _mainMenuModel = new UiMainMenuModel(); 
        public ReactiveProperty<bool> IsMainMenuActive { get; set; }

        private void Start()
        {
            _mainMenu.SetActive(true);
            Subcribe();
        }

        private void Subcribe()
        {
            _quitButton.OnClickAsObservable().Subscribe(_ => _mainMenuModel.QuitApp()).AddTo(this);
            _scoreButton.OnClickAsObservable().Subscribe(_ => 
            {
                _mainMenuModel.HideMenu(gameObject);
                _scoreMenu.gameObject.SetActive(true);
            }).AddTo(this);
            _creditsBtn.OnClickAsObservable().Subscribe(_ =>
            {
                _mainMenuModel.HideMenu(gameObject);
                _creditsMenu.gameObject.SetActive(true);
            }).AddTo(this);
            _startButton.OnClickAsObservable().Subscribe(_ => _mainMenuModel.StartGame()).AddTo(this);
        }
    }
}
