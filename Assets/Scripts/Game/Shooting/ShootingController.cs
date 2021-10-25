using UnityEngine;
using Zenject;
using UniRx;
using System.Collections.Generic;

namespace Clicker
{
    internal sealed class ShootingController : BaseController
    {
        private readonly InputTouchPresenter _inputTouchPresenter;
        private readonly ImpactEffectView.Factory _impactEffectViewFactory;
        private readonly ShootingLineRendererView.Factory _shootingLineRendererViewFactory;
        private readonly List<ShootingLineRendererView> _shootingLineRendererViewList;
        private readonly List<ImpactEffectView> _impactEffectViewList;

        public ShootingController(
            InputTouchPresenter inputTouchPresenter, 
            ShootingLineRendererView.Factory shootingLineRendererViewFactory,
            ImpactEffectView.Factory impactEffectViewFactory)
        {
            _inputTouchPresenter = inputTouchPresenter;
            _shootingLineRendererViewFactory = shootingLineRendererViewFactory;
            _shootingLineRendererViewList = new List<ShootingLineRendererView>();
            _impactEffectViewFactory = impactEffectViewFactory;
            _impactEffectViewList = new List<ImpactEffectView>();
        }

        public override void Start()
        {
            AddViewsToViewsList(_shootingLineRendererViewList, _shootingLineRendererViewFactory, 5);
            AddViewsToViewsList(_impactEffectViewList, _impactEffectViewFactory, 10);
            SubscribeOnInputProperties();
        }

        private void AddViewsToViewsList<T>(List<T> listOfView, IFactory<T> factory, int countToCreate)
        {
            for (int i = 0; i < countToCreate; i++)
            {
                var view = factory.Create();
                listOfView.Add(view);
            }
        }

        private void SubscribeOnInputProperties()
        {
            _inputTouchPresenter.Enemy.Subscribe(enemy => 
            { 
                ShootTheEnemy(
                    CheckIsLineRendereIsFree(_shootingLineRendererViewList),
                    CheckIsImpactEffectIsFree(_impactEffectViewList),
                    enemy);
            });

            _inputTouchPresenter.TouchPosition.Subscribe(position =>
            {
                ShootingAtNothing(
                   CheckIsLineRendereIsFree(_shootingLineRendererViewList),
                   CheckIsImpactEffectIsFree(_impactEffectViewList),
                   position);
            });
        }
        private ShootingLineRendererView CheckIsLineRendereIsFree(List<ShootingLineRendererView> listOfView)
        {
            foreach (var view in listOfView)
            {
                if (!view.InDrawLineProcess)
                    return view;
            }
            return null;
        }

        private ImpactEffectView CheckIsImpactEffectIsFree(List<ImpactEffectView> listOfView)
        {
            foreach (var view in listOfView)
            {
                if (!view.IsPlaying)
                    return view;
            }
            return null;
        }

        private void ShootTheEnemy(
            ShootingLineRendererView lineRendererView, 
            ImpactEffectView impactEffectView, 
            IEnemy enemy)
        {
            if (lineRendererView == null || impactEffectView == null)
                return;

            if (enemy != null)
            {
                lineRendererView.DrawShootLine(Vector3.up, enemy.CurrentPosition);
                impactEffectView.DoImpactEffect(enemy.CurrentPosition);
            }
        }

        private void ShootingAtNothing(
            ShootingLineRendererView lineRendererView, 
            ImpactEffectView impactEffectView, 
            Vector3 pointInSpace)
        {
            if (lineRendererView == null || impactEffectView == null)
                return;

            if (pointInSpace != Vector3.zero)
            {
                lineRendererView.DrawShootLine(Vector3.up, pointInSpace);
                impactEffectView.DoImpactEffect(pointInSpace + Vector3.up);
            }
        }

        internal sealed class Factory : PlaceholderFactory<ShootingController>
        {
        }
    }
}
