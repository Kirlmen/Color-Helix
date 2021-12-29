using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private static bool _isPressing;
    public static bool IsPressing()
    {
        return _isPressing;
    }




    public void OnPointerDown(PointerEventData data)
    {
        _isPressing = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        _isPressing = false;
    }

}
