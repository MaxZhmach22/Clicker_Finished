using UnityEngine;
using Zenject;

namespace Clicker
{
    //TODO LevelHelper!
    public class LevelHelper
    {
        readonly Camera _camera;

        public LevelHelper(
            [Inject(Id = "Main")]
            Camera camera)
        {
            _camera = camera;
           
        }

        public float Bottom
        {
            get { return -ExtentHeight; }
        }

        public float Top
        {
            get { return ExtentHeight; }
        }

        public float Left
        {
            get { return -ExtentWidth; }
        }

        public float Right
        {
            get { return ExtentWidth-1; }
        }

        public float ExtentHeight
        {
            get { return _camera.orthographicSize; }
        }

        public float Height
        {
            get { return ExtentHeight * 2.0f; }
        }

        /// <summary>
        /// Метод возвращающий ширину экрана при умножении отношении камеры(_cavera.aspect) 
        /// на камера
        /// </summary>
        public float ExtentWidth
        {
            get { return _camera.aspect * _camera.orthographicSize; }
        }

        public float Width
        {
            get { return ExtentWidth * 2.0f; }
        }

        public void Print()
        {
            Debug.Log($"Bottom: {Bottom}\nTop{Top}\nLeft{Left}\nRight{Right}\nWidth {Width}\nHeight {Height}\n");
        }
    }
}
