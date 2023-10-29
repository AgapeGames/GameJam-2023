using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickFertilizer : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public TaskFertilizer taskFertilizer;
    public bool isOrganik;

    void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isOrganik)
        {
            taskFertilizer.IncrementTarget();
            gameObject.SetActive(false);
        }
        else
        {
            taskFertilizer.Lose();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
