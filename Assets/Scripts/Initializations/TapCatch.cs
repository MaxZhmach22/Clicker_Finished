using UnityEngine;
using UniRx;


namespace MonsterClicker
{
    internal sealed class TapCatch : IExecute, ITapCatch
    {
        #region Fields

        private Subject<float> _onSelectableTap = new Subject<float>();
        private Touch _touch;


        #endregion

        #region ITapCatch

        public ISubject<float> OnSelectableTap => _onSelectableTap;

        #endregion


        #region UnityMethods

        /// <summary>
        /// TODO Refactor on ObservableUpdate
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Execute(float deltaTime)
        {
            if (Input.touchCount == 1)
                ChooseFingerOnScreen(0);
            if (Input.touchCount >= 2)
                ChooseFingerOnScreen(1);
        }

        #endregion


        #region Methods

        private void ChooseFingerOnScreen(int countOfTouch)
        {
            _touch = Input.GetTouch(countOfTouch);
            switch (_touch.phase)
            {
                case TouchPhase.Ended:
                    RayHitEnemy();
                    break;
            }
        }

        private void RayHitEnemy()
        {
            Vector3 touchPoint = _touch.position;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(touchPoint), out RaycastHit hit, 100f))
            {
                if (hit.collider.TryGetComponent<ISelectable>(out var gameObject))
                {
                    _onSelectableTap?.OnNext(gameObject.ScorePoints);
                    if(gameObject is EnemyBase)
                        gameObject.GetSelected();
                }
            }
        } 
        #endregion
    }
}
