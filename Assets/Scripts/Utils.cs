using UnityEngine;

public static class Utils 
{
    public static Vector3 RandomPosition(float minValue, float maxValue)
    {
        return new Vector3(Random.Range(minValue, maxValue), 0, Random.Range(minValue, maxValue));
    }
}
