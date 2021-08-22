using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeCounter = 2;
    private float counter;
    private Vector3 _playerPosition;
    private float sumOfBounds;

    void Start()
    {
        _playerPosition = GameObject.Find("Player").transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //counter += Time.deltaTime;
        //if(counter >= timeCounter)
        //{
        //    var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    do
        //    {
        //        gameObject.transform.position = new Vector3(Random.Range(-20, 20), 0.5f, Random.Range(-20, 20));
        //        sumOfBounds = Mathf.Abs(gameObject.transform.position.x) + Mathf.Abs(gameObject.transform.position.z);
        //    } while (sumOfBounds > 20);
        //    counter -= timeCounter;
        //}

    }
}
