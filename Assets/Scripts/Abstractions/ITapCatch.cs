using UniRx;


namespace MonsterClicker
{
    internal interface ITapCatch
    {
        ISubject<float> OnSelectableTap { get; }
    }
}