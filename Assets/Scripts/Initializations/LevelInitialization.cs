using UnityEngine;


namespace MonsterClicker
{
    internal sealed class LevelInitialization : ILevelInitialization
    {
        #region ClassLifeCycles

        public LevelInitialization(GameData gameData, Camera camera)
        {
            CreateSurface(gameData);
            SetCameraSettings(camera, gameData);
        }

        #endregion


        #region ILevelInitialization

        public void SetCameraSettings(Camera camera, GameData gameData)
        {
            var cameraParent = new GameObject("Camera Parent");
            cameraParent.transform.rotation = Quaternion.Euler(gameData.CameraRotation);
            camera.transform.SetParent(cameraParent.transform);
            camera.orthographic = gameData.IsOrthographic;
            camera.orthographicSize = gameData.OrthographicSize;
            camera.nearClipPlane = gameData.NearClipPlaneSize;
            camera.farClipPlane = gameData.FarClipPlaneSize;
            camera.transform.localPosition = gameData.CameraLocalPosition;
        }

        public void CreateSurface(GameData gameData)
        {
            var plane = GameObject.CreatePrimitive(gameData.Type);
            plane.transform.localScale = gameData.Size;
            var meshRender = plane.GetComponent<MeshRenderer>();
            meshRender.material = gameData.SurfaceMaterial;
            GameObject.Instantiate(
                gameData.GameBorders, 
                Vector3.zero, 
                Quaternion.Euler(gameData.BordersRotation));
        } 

        #endregion
    }
}