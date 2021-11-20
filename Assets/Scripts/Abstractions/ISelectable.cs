namespace MonsterClicker
{
    internal interface ISelectable
    {
        float ScorePoints { get; }
        void GetSelected(float ?value = null);
    }
}