using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Clicker
{
    internal abstract class BaseController : IDisposable
    {
        private List<BaseController> _baseControllers;
        private List<GameObject> _gameObjects;

        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
                return;

            DisposeBaseControllers();
            DisposeGameObjects();

            OnDispose();
            _isDisposed = true;
        }

        public abstract void Start();

        private void DisposeBaseControllers()
        {
            if (_baseControllers == null)
                return;

            foreach (BaseController baseController in _baseControllers)
                baseController.Dispose();

            _baseControllers.Clear();
        }

        private void DisposeGameObjects()
        {
            if (_gameObjects == null)
                return;

            foreach (GameObject gameObject in _gameObjects)
                Object.Destroy(gameObject);

            _gameObjects.Clear();
        }

        protected void AddController(BaseController baseController)
        {
            _baseControllers ??= new List<BaseController>();
            _baseControllers.Add(baseController);
        }

        protected void AddGameObject(GameObject gameObject)
        {
            _gameObjects ??= new List<GameObject>();
            _gameObjects.Add(gameObject);
        }

        protected virtual void OnDispose() { }
    }
}