using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4f;
    private FloatingJoystick _joystick;
    private Vector3 _boundaries;
    private Vector3 _forward, _right;

    void Start()
    {
        _joystick = FindObjectOfType<FloatingJoystick>();
        
        _forward = Camera.main.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;
    }

    public void SetBoundaries(Vector3 screenBoundaries)
    {
        _boundaries = screenBoundaries;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Math.Abs(_joystick.Vertical + _joystick.Horizontal) > 0)
        {
            Move();
        }
        
        //if (_joystick.Horizontal > _joystick.DeadZone || _joystick.Vertical > _joystick.DeadZone)
        //{
           
        //}
    }

    private void Move()
    {
        float screenBounds = transform.position.x + transform.position.z;
        Vector3 direction = new Vector3((_joystick.Horizontal), 0, (_joystick.Vertical));
        Vector3 rigthMovement = _right * _movementSpeed * Time.deltaTime * _joystick.Horizontal;
        Vector3 upMovement = _forward * _movementSpeed * Time.deltaTime * _joystick.Vertical;
        Vector3 heading = Vector3.Normalize(rigthMovement + upMovement);

        transform.forward = heading;
        transform.position += rigthMovement;
        transform.position += upMovement;

    }

}   
