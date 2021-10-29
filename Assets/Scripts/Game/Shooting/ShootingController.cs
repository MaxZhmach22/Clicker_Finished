using UnityEngine;
using Zenject;
using UniRx;
using System.Collections.Generic;
using System.Linq;

namespace Clicker
{
    internal sealed class ShootingController : BaseController
    {
        private readonly InputTouchPresenter _inputTouchPresenter;
        private readonly ImpactEffectView.Factory _impactEffectViewFactory;
        private readonly ExplosionForceEffect.Factory _explosionForceEffectFactory;
        private readonly ShootingLineRendererView.Factory _shootingLineRendererViewFactory;
        private readonly List<ShootingLineRendererView> _shootingLineRendererViewList;
        private readonly List<ImpactEffectView> _impactEffectViewList;
        private readonly List<ExplosionForceEffect> _explosionForceEffectViewList;

        private int _viewsCount = 7;
        private int _explosionViewsCount = 1;
        public ShootingController(
            InputTouchPresenter inputTouchPresenter, 
            ShootingLineRendererView.Factory shootingLineRendererViewFactory,
            ImpactEffectView.Factory impactEffectViewFactory,
            ExplosionForceEffect.Factory explosionForceEffectFactory)
        {
            _inputTouchPresenter = inputTouchPresenter;
            _shootingLineRendererViewFactory = shootingLineRendererViewFactory;
            _shootingLineRendererViewList = new List<ShootingLineRendererView>();
            _impactEffectViewFactory = impactEffectViewFactory;
            _impactEffectViewList = new List<ImpactEffectView>();
            _explosionForceEffectFactory = explosionForceEffectFactory;
            _explosionForceEffectViewList = new List<ExplosionForceEffect>();
        }

        public override void Start()
        {
            AddViewsToViewsReUseList(_shootingLineRendererViewList, _shootingLineRendererViewFactory, _viewsCount);
            AddViewsToViewsReUseList(_impactEffectViewList, _impactEffectViewFactory, _viewsCount);
            AddViewsToViewsReUseList(_explosionForceEffectViewList, _explosionForceEffectFactory, _explosionViewsCount);
            SubscribeOnInputProperties();
        }

        private void AddViewsToViewsReUseList<T>(List<T> listOfView, IFactory<T> factory, int countToCreate)
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
                    CheckExplosionForseEffectIsFree(_explosionForceEffectViewList),
                    enemy);
            });

            _inputTouchPresenter.TouchPosition.Subscribe(position =>
            {
                ShootingAtNothing(
                   CheckIsLineRendereIsFree(_shootingLineRendererViewList),
                   CheckIsImpactEffectIsFree(_impactEffectViewList),
                   CheckExplosionForseEffectIsFree(_explosionForceEffectViewList),
                   position);
            });
        }
        private ExplosionForceEffect CheckExplosionForseEffectIsFree(List<ExplosionForceEffect> listOfView)
        {
            listOfView ??= new List<ExplosionForceEffect>();
            return listOfView.FirstOrDefault();
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
            ExplosionForceEffect explosionForceEffect,
            IEnemy enemy)
        {
            if (lineRendererView == null || impactEffectView == null || explosionForceEffect == null)
                return;

            if (enemy != null)
            {
                lineRendererView.DrawShootLine(Vector3.up, enemy.CurrentPosition);
                impactEffectView.DoImpactEffect(enemy.CurrentPosition);
                explosionForceEffect.CreateExplosionEffect(
                    enemy.CurrentPosition, false, 
                    enemy.ExplosionForceCoefficient, 
                    enemy.ExplosionRadiusCoefficient);
            }
        }

        private void ShootingAtNothing(
            ShootingLineRendererView lineRendererView, 
            ImpactEffectView impactEffectView, 
            ExplosionForceEffect explosionForceEffect,
            Vector3 pointInSpace)
        {
            if (lineRendererView == null || impactEffectView == null)
                return;

            if (pointInSpace != Vector3.zero)
            {
                lineRendererView.DrawShootLine(Vector3.up, pointInSpace);
                impactEffectView.DoImpactEffect(pointInSpace + Vector3.up);
                explosionForceEffect.CreateExplosionEffect(pointInSpace + Vector3.up, true);
            }
        }

        internal sealed class Factory : PlaceholderFactory<ShootingController>
        {
        }
    }
}
