using UniRx;
using Zenject;
using DG.Tweening;

namespace Clicker
{
    internal class CreditsMenuController : BaseUiController
    {
        private readonly CreditsMenuView.Factory _creditsMenuViewFactory;
        private CreditsMenuView _creditsMenuView;
        private readonly Player _player;

        public CreditsMenuController(CreditsMenuView.Factory settingsMenuViewFactory, Player player)
        {
            _creditsMenuViewFactory = settingsMenuViewFactory;
            _player = player;
        }
        public override void Start()
        {
            _creditsMenuView = _creditsMenuViewFactory.Create();
            AddGameObject(_creditsMenuView.gameObject);
            _creditsMenuView.BackBtn.OnClickAsObservable().Subscribe(_ => 
            {
                //TODO 1. DOTween;
                _player.ChangeState(GameStates.Start); 
            });
        }
        public class Factory : PlaceholderFactory<CreditsMenuController>
        {
        }
    }
}