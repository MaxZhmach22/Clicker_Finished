using UniRx;


namespace MonsterClicker
{
    internal interface IScoreCounter
    {
        IReadOnlyReactiveProperty<float> CurrentScore { get; }
    }
}