using UnityEngine;

namespace MonsterClicker
{
    internal interface ILevelInitialization
    {
        void CreateSurface(GameData gameData);

        void SetCameraSettings(Camera camera, GameData gameData);
    }
}