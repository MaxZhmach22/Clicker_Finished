using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{
    internal class ScoreListInitialization : IScoreSaver
    {
        public event Action OnListClear;
        private const string KEY = "Score";
        private readonly GameData _gameData;
        private readonly List<float> _scoreList;

        public ScoreListInitialization(GameData gameData)
        {
            _gameData = gameData;
            _scoreList = new List<float>();
            GetFloatsFromPlayerPrefs();
        }

        private void GetFloatsFromPlayerPrefs()
        {
            for (int i = 0; i <= _gameData.BestScoreListCount; i++)
            {
                var score = PlayerPrefs.GetFloat($"{KEY}{i}");
                if (score != 0)
                    _scoreList.Add(score);
            }
        }

        public void SaveScore(float value)
        {
            for (int i = 0; i <= _gameData.BestScoreListCount + 1; i++)
            {
                if(!PlayerPrefs.HasKey($"{KEY}{i}"))
                    PlayerPrefs.SetFloat($"{KEY}{i}", value);
            }
            PlayerPrefs.Save();
        }

        public void ResetScoreList()
        {
            PlayerPrefs.DeleteAll();
            _scoreList.Clear();
            OnListClear?.Invoke();
        }
    }
}