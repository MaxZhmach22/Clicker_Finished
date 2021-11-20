using System;

namespace MonsterClicker
{
    internal interface IScoreChanged
    {
        event Action<float> OnScoreChanged;
    }
}