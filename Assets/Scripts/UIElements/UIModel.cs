using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace MonsterClicker
{
    internal sealed class UiModel
    {
        private GameObject _mainMenu;
        private GameObject _creditsMenu;
        private GameObject _scoreMenu;
        private GameObject _gameBtnMenu;
        private GameObject _looseMenu;
        private List<GameObject> _menuList;
        public Action OnClickSound;
        public Action OnStartSound;
        public Action OnShowBestScore;
        private GameData _gameData;
        

        public GameObject MainMenu { get => _mainMenu;  }
        public GameObject CreditsMenu { get => _creditsMenu;  }
        public GameObject ScoreMenu { get => _scoreMenu;}
        public GameObject GameBtnMenu { get => _gameBtnMenu; }
        public GameObject LooseMenu { get => _looseMenu; }

        public UiModel(GameData gameData)
        {
            _gameData = gameData;
            FindReferences();
            SetStartPosition();
        }

        private void SetStartPosition()
        {
            _mainMenu.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            _scoreMenu.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            _creditsMenu.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            _looseMenu.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        private void FindReferences()
        {
            _mainMenu = GameObject.Find("MainMenu");
            _creditsMenu = GameObject.Find("CreditsMenu");
            _scoreMenu = GameObject.Find("ScoreMenu");
            _looseMenu = GameObject.Find("LooseMenu");
            _gameBtnMenu = GameObject.Find("BntInGame");
            _menuList = new List<GameObject>() {_mainMenu, _gameBtnMenu, _creditsMenu, _scoreMenu, _looseMenu };
            
        }

        public void SetAcitiveOrDisable(GameObject menuObj)
        {
            var status = menuObj.activeSelf;
            status = !status;
            menuObj.SetActive(status);
        }
        public void StartGame()
        {

            HideMenus();
            GameBtnMenu.SetActive(true);
            GameTime.StopStartGame();
            OnStartSound?.Invoke();
        }

        public void ShowScoreMenu()
        {
            HideMenus();
            ScoreMenu.SetActive(true);
            OnShowBestScore?.Invoke();
            OnClickSound?.Invoke();
        }

        public void ShowLooseMenu(bool gameOver)
        {
            if (gameOver)
            {
                HideMenus();
                _looseMenu.SetActive(true);
                OnClickSound?.Invoke();
                Time.timeScale = 0;
            }
            
        }

        public void ShowCreditsMenu()
        {
            HideMenus();
            CreditsMenu.SetActive(true);
            OnClickSound?.Invoke();
        }

        public void ShowStartGameMenu()
        {
            HideMenus();
            MainMenu.SetActive(true);
            OnClickSound?.Invoke();
        }

        public void OnGameStart()
        {
            HideMenus();
            MainMenu.SetActive(true);
            Time.timeScale = 0;
        }

        private void HideMenus()
        {
            foreach (var menu in _menuList)
                menu.SetActive(false);
        }

        public void PauseGame()
        {
           
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }

        public void QuitApp() =>
            Application.Quit();

    }
}
