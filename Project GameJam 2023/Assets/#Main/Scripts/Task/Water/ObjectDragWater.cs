using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDragWater : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 initialPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        initialPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    
}
