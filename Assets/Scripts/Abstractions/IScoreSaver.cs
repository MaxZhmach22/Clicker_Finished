using System;

namespace MonsterClicker
{
    internal interface IScoreSaver
    {
        event Action OnListClear;
        void SaveScore(float value);
    }
}