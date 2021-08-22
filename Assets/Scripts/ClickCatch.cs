using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCatch : MonoBehaviour
{

    private FloatingJoystick _joystick;
    private Touch _touch;
    private RectTransform _rectTransform;
    private float _deadZoneX;
    private float _deadZoneY;

    // Start is called before the first frame update
    void Start()
    {
        _joystick = FindObjectOfType<FloatingJoystick>();
        _rectTransform = _joystick.GetComponent<RectTransform>();

        _deadZoneX = _joystick.transform.position.x + _rectTransform.sizeDelta.x / 2;
        _deadZoneY = _joystick.transform.position.y + _rectTransform.sizeDelta.y / 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.touchCount == 1)
        //{
        //    _touch = Input.GetTouch(0);
        //    RayHitEnemy();
        //    //Debug.Log(Input.mousePosition);
        //    //var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    //gameObject.transform.position = hit.point + Vector3.up;
        //    //Debug.Log(hit.point);
        //}
        //if(Input.touchCount >= 2)
        //{
        //    _touch = Input.GetTouch(1);
        //    RayHitEnemy();
        //}
    }

    private void RayHitEnemy()
    {
        RaycastHit hit;
        Vector3 touchPoint = _touch.position;
        Physics.Raycast(Camera.main.ScreenPointToRay(touchPoint), out hit, 100f);
        if (hit.collider.TryGetComponent<EnemyMove>(out EnemyMove enemy))
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
