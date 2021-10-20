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
        private IEnemy _enemy;
        
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
        private ImpactEffectView CheckIsImpactEffectIsFree(List<ImpactEffectView> listOfView)
        {
            foreach (var view in listOfView)
            {
                if (!view.IsPlaying)
                    return view;
            }
            return null;
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
        private void SubscribeOnInputProperties()
        {
            _inputTouchPresenter.Enemy.Subscribe(enemy => { _enemy = enemy; if(enemy != null) Debug.Log(enemy.ToString()); });
            _inputTouchPresenter.TouchPosition.Subscribe(position =>
            {
                if (position !=  Vector3.zero)
                {
                    var lineRendererView = CheckIsLineRendereIsFree(_shootingLineRendererViewList);
                    var impactEffectView = CheckIsImpactEffectIsFree(_impactEffectViewList);
                    if (lineRendererView != null && impactEffectView != null)
                    {
                        lineRendererView.DrawShootLine(Vector3.zero, position);
                        impactEffectView.DoImpactEffect(position + Vector3.up);
                        if (_enemy != null)
                        {
                            
                        }
                    }
                }
            });
        }


        internal sealed class Factory : PlaceholderFactory<ShootingController>
        {
        }
    }
}
