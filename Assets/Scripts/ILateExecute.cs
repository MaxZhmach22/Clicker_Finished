﻿namespace MonsterClicker
{
    public interface ILateExecute : IController
    {
        void LateExecute(float deltaTime);
    }
}