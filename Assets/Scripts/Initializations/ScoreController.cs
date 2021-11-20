using UniRx;


namespace MonsterClicker
{
    internal sealed class ScoreController : BaseController
    {
        #region Fields

        private readonly ITapCatch _tapCatch;
        private readonly ScoreCounter _scoreCounter;
        private readonly ScoreListInitialization _scoreSaver;
        private readonly CompositeDisposable _disposables; 

        #endregion


        #region ClassLifeCycles

        public ScoreController(
            ITapCatch tapCatch,
            ScoreListInitialization scoreSaver,
            ScoreCounter scoreCounter)
        {
            _tapCatch = tapCatch;
            _scoreSaver = scoreSaver;
            _scoreCounter = scoreCounter;
            _disposables = new CompositeDisposable();
        }

        public override void Start() =>
            Subcribe();
        public override void Dispose() =>
            _disposables.Clear();


        #endregion


        #region Methods

        private void Subcribe() =>
             _tapCatch.OnSelectableTap.Subscribe(value => _scoreCounter.CountScore(value)).AddTo(_disposables);

        #endregion
    }
}