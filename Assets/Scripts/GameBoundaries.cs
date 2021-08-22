using UnityEngine;

public class GameBoundaries
{
    private Vector3 _screenBoundaries;

    public Vector3 ScreenBoundaries { get => _screenBoundaries; }

    public GameBoundaries(Camera mainCamera)
    {
        _screenBoundaries = new Vector3(mainCamera.orthographicSize, 0, mainCamera.orthographicSize * 2);
    }
}
