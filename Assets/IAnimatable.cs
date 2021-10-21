namespace Clicker
{
    internal interface IAnimatable
    {
        bool AnimationInProcess { get; }
        void PlayAnimation();
    }
}