using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCreator : MonoBehaviour
{
    [SerializeField] private GameObject _zombie;
    [SerializeField] private int _zombieCounts;
 
    void Start()
    {
        var parent = new GameObject("Zombies");
        for (int i = 0; i < _zombieCounts; i++)
        {
            GameObject.Instantiate(_zombie, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity, parent.transform);
        }
    }

}
