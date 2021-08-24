using MonsterClicker;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal sealed class UIView 
{
    private Button _quit;
    private Button _scoreMenuBtn;
    private Button _creditsMenuBtn;
    private Button _startGame;
    private Button _backScoreBtn;
    private Button _backCreditsBtn;
    private Button _pauseBtn;
    private Button _muteBtn;
    private Button _backLooseMenuBtn;
    private Button _clearScoreBtn;
    private TextMeshProUGUI _textScoreList;
    private TextMeshProUGUI _currentScoreLooseMenuTxt;
    private TextMeshProUGUI _currentScoreInGameTXT;
    private Score _score;
    


    public Button Quit { get => _quit; }
    public Button ScoreMenuBtn { get => _scoreMenuBtn; }
    public Button StartGame { get => _startGame; }
    public Button BackScoreBtn { get => _backScoreBtn; }
    public Button BackCreditsBtn { get => _backCreditsBtn; }
    public Button ClearScoreBtn { get => _clearScoreBtn; }
    public Button PauseBtn { get => _pauseBtn; }
    public Button MuteBtn { get => _muteBtn; }
    public Button CreditsMenuBtn { get => _creditsMenuBtn; }
    public Button BackLooseMenuBtn { get => _backLooseMenuBtn; }
    public TextMeshProUGUI TextScoreList { get => _textScoreList; }

    public UIView(Score score)
    {
        _score = score;
        FindReferences();

    }

    private void FindReferences()
    {
        _quit = GameObject.Find("QuitBtn").GetComponent<Button>();
        _scoreMenuBtn = GameObject.Find("ScoreBtn").GetComponent<Button>();
        _startGame = GameObject.Find("StartBtn").GetComponent<Button>();
        _creditsMenuBtn = GameObject.Find("CreditsBtn").GetComponent<Button>();
        _backScoreBtn = GameObject.Find("BackScoreBtn").GetComponent<Button>();
        _backLooseMenuBtn = GameObject.Find("BackLooseMenuBtn").GetComponent<Button>();
        _backCreditsBtn = GameObject.Find("BackCreditsBtn").GetComponent<Button>();
        _clearScoreBtn = GameObject.Find("ClearScoreBtn").GetComponent<Button>();
        _pauseBtn = GameObject.Find("PauseBtn").GetComponent<Button>();
        _muteBtn = GameObject.Find("MuteBtn").GetComponent<Button>();
        _textScoreList = GameObject.Find("ScoreList").GetComponent<TextMeshProUGUI>();
        _currentScoreInGameTXT = GameObject.Find("CurrentScoreInGameTXT").GetComponent<TextMeshProUGUI>();
        _currentScoreLooseMenuTxt = GameObject.Find("CurrentScoreTxt").GetComponent<TextMeshProUGUI>();
    }

    public void PrintCurrentScore(int score)
    {
        _currentScoreInGameTXT.text = "Score: " + score.ToString();
    }
    public void PrintCurrentScoreInLooseMenu(bool gameOver)
    {
        if (gameOver)
        {
            _currentScoreLooseMenuTxt.text = _currentScoreInGameTXT.text;
        }
    }

    public void PrintBestScore()
    {
        _textScoreList.text = "";
        for (int i = 0; i < _score._bestScoreList.Count; i++)
        {
            _textScoreList.text += i + 1 + ":" + " " + _score._bestScoreList[i] + "\n";
        }
    }
}
