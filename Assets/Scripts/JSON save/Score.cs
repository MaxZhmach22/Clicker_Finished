using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker
{
    [Serializable]
    internal sealed class Score
    {
        public List<int> _bestScoreList = new List<int>();
        [NonSerialized] public int _currentScore = 0;
    }
}