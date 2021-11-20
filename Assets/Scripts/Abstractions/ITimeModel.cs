using System;


namespace MonsterClicker
{
    internal interface ITimeModel
    {
        IObservable<int> GameTime { get; }
    }
}