using UnityEngine;

namespace MonsterClicker
{
    internal sealed class UiMainMenuModel : IUiModel
    {

        public void SetAcitiveOrDisable(GameObject menuObj)
        {
            var status = menuObj.activeSelf;
            status = !status;
            menuObj.SetActive(status);
        }

        public void StartGame()
        {
            GameTime.StopStartGame();
        }

        //public void ShowScoreMenu()
        //{
        //    HideMenus();
        //    ScoreMenu.SetActive(true);
        //    OnShowBestScore?.Invoke();
        //    OnClickSound?.Invoke();
        //}

        //public void ShowLooseMenu(bool gameOver)
        //{
        //    if (gameOver)
        //    {
        //        HideMenus();
        //        _looseMenu.SetActive(true);
        //        OnClickSound?.Invoke();
        //        Time.timeScale = 0;
        //    }

        //}

        //public void ShowCreditsMenu()
        //{
        //    HideMenus();
        //    CreditsMenu.SetActive(true);
        //    OnClickSound?.Invoke();
        //}

        //public void ShowStartGameMenu()
        //{
        //    HideMenus();
        //    MainMenu.SetActive(true);
        //    OnClickSound?.Invoke();
        //}

        //public void OnGameStart()
        //{
        //    HideMenus();
        //    MainMenu.SetActive(true);
        //    Time.timeScale = 0;
        //}

        //private void HideMenus()
        //{
        //    foreach (var menu in _menuList)
        //        menu.SetActive(false);
        //}

        //public void PauseGame()
        //{

        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //}

        public void QuitApp() =>
            Application.Quit();

        public void HideMenu(GameObject gameObject) =>
            gameObject.SetActive(false);

    }
}
