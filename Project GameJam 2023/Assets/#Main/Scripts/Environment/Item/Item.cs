using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject interactionPopup;

    public TypeItem typeItem;

    public void GetItem()
    {
        if(typeItem == TypeItem.SCRAPS)
        {

        }else if (typeItem == TypeItem.BATTERY)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionPopup.SetActive(false);
        }
    }
}

public enum TypeItem
{ 
    SCRAPS,
    BATTERY
}

