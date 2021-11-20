using UniRx;


namespace MonsterClicker
{
    internal sealed class ScoreCounter : IScoreCounter
    {
        #region Fields

        private ReactiveProperty<float> _currentScore = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<float> CurrentScore => _currentScore;

        #endregion


        #region Methods

        public void CountScore(float value) =>
            _currentScore.Value += value;
   
        #endregion
    }
}