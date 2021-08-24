using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{
    internal static class GameTime 
    {
        public static bool isStoped;
        public static Action<bool> OnStopStartGame;
        public static void StopStartGame()
        {
            if(Time.timeScale == 0)
            {
                isStoped = false;
                Time.timeScale = 1f;
                OnStopStartGame?.Invoke(isStoped);
            }
            if(Time.timeScale < 0 )
            {
                isStoped = true;
                Time.timeScale = 0;
                OnStopStartGame?.Invoke(isStoped);
            }
        }
    }
}
