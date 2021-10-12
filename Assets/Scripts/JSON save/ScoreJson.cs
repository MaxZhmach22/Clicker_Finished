using System;
using System.IO;
using UnityEngine;

namespace Clicker
{
    internal sealed class ScoreJson
    {
        private Score _score;
        private string _filePath;
        public Action OnResetScore;
        public Action<int> OnScoreChange;

        internal Score Score { get => _score; }

        public ScoreJson()
        {
            _score = new Score();
            _filePath = GetPath();
        }

        private void SaveToJson()
        {
            string json = JsonUtility.ToJson(_score);
            File.WriteAllText(_filePath, json);
        }

        public void ResetBestScore()
        {
            _score._bestScoreList.Clear();
            SaveToJson();
            OnResetScore?.Invoke();
        }

        public void CurrentScore(int oneTap)
        {
            _score._currentScore += oneTap;
            OnScoreChange?.Invoke(_score._currentScore);
        }

        public void IsTheBestScore(bool gameOver)
        {
            if (gameOver)
            {
                _score._bestScoreList.Add(_score._currentScore);

                for (int i = 0; i < _score._bestScoreList.Count; i++)
                {
                    if (_score._currentScore >= _score._bestScoreList[i])
                    {
                        _score._bestScoreList.Sort();
                        _score._bestScoreList.Reverse();
                        if (_score._bestScoreList.Count >= 6)
                        {
                            _score._bestScoreList.Remove(_score._bestScoreList.Count-1);
                        }
                    }

                }
                SaveToJson();
            }
            
        }

        public string GetPath()
        {
            string saveFolder = (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ? Application.persistentDataPath : Application.dataPath) + "/Score save/";
            string filePath = saveFolder + "Score.json";
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                File.WriteAllText(filePath, JsonUtility.ToJson(_score));
            }
            else
            {
                string jsonText = File.ReadAllText(filePath);
                _score = JsonUtility.FromJson<Score>(jsonText);
            }
            return filePath;
        }

    }
}