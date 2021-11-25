using UniRx;

namespace MonsterClicker
{
    internal interface IForcePowerLevel
    {
        IReadOnlyReactiveProperty<float> MoveForcePower { get; }
    }
}