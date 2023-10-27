using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject interactionPopup;

    public TypeItem typeItem;

    public void GetItem()
    {
        if(typeItem == TypeItem.AMMO)
        {

        }else if (typeItem == TypeItem.SCRAB)
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
    AMMO,
    SCRAB
}

