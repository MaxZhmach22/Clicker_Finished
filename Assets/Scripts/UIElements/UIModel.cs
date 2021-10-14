using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Clicker
{
    public class UIModel
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
        private GameSettingsInstaller _gameData;
        

        public GameObject MainMenu { get => _mainMenu;  }
        public GameObject CreditsMenu { get => _creditsMenu;  }
        public GameObject ScoreMenu { get => _scoreMenu;}
        public GameObject GameBtnMenu { get => _gameBtnMenu; }
        public GameObject LooseMenu { get => _looseMenu; }

        public UIModel(GameSettingsInstaller gameData)
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

            foreach (var menu in _menuList)
            {
                menu.SetActive(false);
            }
            GameBtnMenu.SetActive(true);
            GameTime.StopStartGame();
            OnStartSound?.Invoke();
        }

        public void ShowScoreMenu()
        {
            foreach(var menu in _menuList)
            {
                menu.SetActive(false);
            }
            ScoreMenu.SetActive(true);
            OnShowBestScore?.Invoke();
            OnClickSound?.Invoke();
        }

        public void ShowLooseMenu(bool gameOver)
        {
            if (gameOver)
            {
                foreach (var menu in _menuList)
                {
                    menu.SetActive(false);
                }
                _looseMenu.SetActive(true);
                OnClickSound?.Invoke();
                Time.timeScale = 0;
            }
            
        }

        public void ShowCreditsMenu()
        {
            foreach (var menu in _menuList)
            {
                menu.SetActive(false);
            }
            CreditsMenu.SetActive(true);
            OnClickSound?.Invoke();
        }

        public void ShowStartGameMenu()
        {
            foreach (var menu in _menuList)
            {
                menu.SetActive(false);
            }
            MainMenu.SetActive(true);
            OnClickSound?.Invoke();
        }

        public void OnGameStart()
        {
            foreach (var menu in _menuList)
            {
                menu.SetActive(false);
            }
            MainMenu.SetActive(true);
            Time.timeScale = 0;
        }

        public void PauseGame()
        {
           
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }

        public void QuitApp()
        {
            Application.Quit();
        }
    }
}
