using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDragWater : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public TaskWater taskWater;
    public int index;

    private Vector2 initialPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        initialPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        taskWater.Done(index);

    }

}
