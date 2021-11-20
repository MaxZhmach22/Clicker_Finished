using UnityEngine;


namespace MonsterClicker
{
    internal sealed class UiViewsLoader 
    {
        private readonly GameUiPresenter _gameUiPresenter;
        private readonly MainMenuPresenter _uiMainMenuPresenter;
        private readonly LooseMenuPresenter _looseMenuPresenter;
        private readonly ScoreMenuPresenter _scoreMenuPresenter;
        private readonly CreditsMenuPresenter _creditsMenuPresenter;
        private readonly GameData _gameData;
        private readonly Transform _placeForUi;

        public UiViewsLoader(GameData gameData, Transform placeForUi)
        {
            _gameData = gameData;
            _placeForUi = placeForUi;
            _gameUiPresenter = LoadPresenter(_gameData.GameUiView, _placeForUi);
            //_uiMainMenuPresenter = LoadPresenter(_gameData.MainMenuPresenter, _placeForUi);
            //_looseMenuPresenter = LoadPresenter(_gameData.LooseMenuPresenter, _placeForUi);
            //_creditsMenuPresenter = LoadPresenter(_gameData.CreditsMenuPresenter, _placeForUi);
            //_scoreMenuPresenter = LoadPresenter(_gameData.ScoreMenuPresenter, _placeForUi);
        }

        private T LoadPresenter<T>(T prefab, Transform palceForUi) where T : BasePresenter =>
            GameObject.Instantiate<T>(prefab, _placeForUi);
    }
}