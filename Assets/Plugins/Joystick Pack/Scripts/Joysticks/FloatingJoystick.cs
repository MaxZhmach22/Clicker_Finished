using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

public class FloatingJoystick : Joystick
{
    public Subject<Vector3> DirectionToMove = new Subject<Vector3>();
    public bool StartIncrease { get; private set; }

    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        StartIncrease = true;
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        DirectionToMove?.OnNext(Direction);
        StartIncrease = false;
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}