using UniRx;
using Zenject;
using DG.Tweening;

namespace Clicker
{
    internal class CreditsMenuController : BaseController
    {
        private readonly CreditsMenuView _creditsMenuView;
        private readonly Player _player;

        public CreditsMenuController(CreditsMenuView creditsMenuView, Player player)
        {
            _creditsMenuView = creditsMenuView;
            _player = player;
        }
        public override void Start()
        {
            _creditsMenuView.gameObject.SetActive(true);
        
            _creditsMenuView.BackBtn.OnClickAsObservable().Subscribe(_ =>
            {
                //TODO 1. DOTween;
                _player.ChangeState(GameStates.Start);
            }).AddTo(_creditsMenuView);
        }

        public override void Dispose()
        {
            _creditsMenuView.gameObject.SetActive(false);
        }

        public class Factory : PlaceholderFactory<CreditsMenuController>
        {
        }
    }
}