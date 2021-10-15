using System;
using UnityEngine;
using Zenject;

namespace Clicker
{
    internal sealed class AudioHandler : IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;
        readonly Settings _settings;
        readonly AudioSource _audioSource;

        public AudioHandler(
            AudioSource audioSource,
            Settings settings,
            SignalBus signalBus)
        {
            _signalBus = signalBus;
            _settings = settings;
            _audioSource = audioSource;
        }
        public void Initialize()
        {
            //_signalBus.Subscribe<CLickButtonSignal>(OnShipCrashed);
        }

        public void Dispose()
        {
            //_signalBus.Unsubscribe<ShipCrashedSignal>(OnShipCrashed);
        }


        void OnClickStartButton()
        {
            _audioSource.PlayOneShot(_settings.StartButtonSound);
        }

        void OnClickButton()
        {
            _audioSource.PlayOneShot(_settings.ClickButtonSound);
        }

        void OnStartApp()
        {
            _audioSource.PlayOneShot(_settings.BackGroundMusic);
        }

        [Serializable]
        public class Settings
        {
            public AudioClip ClickButtonSound;
            public AudioClip StartButtonSound;
            public AudioClip BackGroundMusic;
        }
    }
}
