using UnityEngine;
using Zenject;
using TMPro;

namespace Clicker
{
    public sealed class GameUiView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void Start() =>
           gameObject.SetActive(false);

        private bool isAnimationInProcess;
        public void SetText(string score)
        {
            _scoreText.text = $"{score}";
            if (isAnimationInProcess)
                return;

            LeanTween.scale(_scoreText.gameObject, Vector3.one, 0.3f)
                .setFrom(Vector3.one * 1.5f)
                .setEase(LeanTweenType.easeOutBack)
                .setOnStart(() => isAnimationInProcess = true)
                .setOnComplete(() => isAnimationInProcess = false);

            //_scoreText.transform.DOScale(
            //    Vector3.one * 0.8f, 0.3f)
            //    .SetEase(Ease.InBack)
            //    .OnComplete(() => _scoreText.transform.localScale = Vector3.one);

            
        }

        

    }
}