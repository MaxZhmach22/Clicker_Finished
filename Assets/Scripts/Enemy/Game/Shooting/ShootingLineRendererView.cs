using UnityEngine;
using Zenject;
using DG.Tweening;

namespace Clicker
{
    public sealed class ShootingLineRendererView : MonoBehaviour
    {
        //private WeaponsPrefs _weaponsPrefs;
        //private PlayerModel _playerModel;

        ////TODO Shooting 1) 
        //[Inject]
        //private void Init(WeaponsPrefs weaponsPrefs, PlayerModel playerModel)
        //{
        //    _weaponsPrefs = weaponsPrefs;

        //}
        public bool InDrawLineProcess { get; private set; } = false;

        private LineRenderer _lineRenderer;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.enabled = false;
        }

        public void DrawShootLine(Vector3 startPos, Vector3 endPosition)
        {

            if(_lineRenderer != null)
            {
                InDrawLineProcess = true;
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, startPos);
                _lineRenderer.SetPosition(1, endPosition + Vector3.up);
                _lineRenderer.DOColor(
                    new Color2(Color.white, Color.white), 
                    new Color2(Color.black, Color.red), 0.5f)   
                    .OnComplete(() => { _lineRenderer.enabled = false; InDrawLineProcess = false;  });
            }
        }
     
        public sealed class Factory : PlaceholderFactory<ShootingLineRendererView>
        {
        }
    }
}