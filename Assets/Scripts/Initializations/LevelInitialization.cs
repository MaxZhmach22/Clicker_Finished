using UnityEngine;

namespace MonsterClicker
{
    internal sealed class LevelInitialization
    {
        
        public LevelInitialization(GameData gameData, Camera camera)
        {
            var plane =  GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.localScale = gameData.PlaneSize;
            var meshRender = plane.GetComponent<MeshRenderer>();
            meshRender.material = gameData.PlaneMaterial;
            var cameraParent = new GameObject("Camera Parent");
            cameraParent.transform.rotation = Quaternion.Euler(new Vector3(30, 45, 0));
            camera.transform.SetParent(cameraParent.transform);
            camera.orthographic = true;
            camera.orthographicSize = 8;
            camera.nearClipPlane = 0.01f;
            camera.farClipPlane = 100;
            camera.transform.localPosition = new Vector3(0, 0, -50);
            GameObject.Instantiate(gameData.GameBorders, Vector3.zero, Quaternion.Euler(0, 45, 0));
        }
    }
}