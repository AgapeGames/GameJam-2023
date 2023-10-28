using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject interactionPopup;

    public TypeItem typeItem;
    public int min, max;

    public void GetItem()
    {
        if(typeItem == TypeItem.SCRAPS)
        {
            ResourceManager.Instance.Scraps(Random.Range(min,max));
        }else if (typeItem == TypeItem.BATTERY)
        {
            ResourceManager.Instance.Battery(Random.Range(min, max));
        }
        else if (typeItem == TypeItem.WATER)
        {
            ResourceManager.Instance.Water(Random.Range(min, max));
        }
        else if (typeItem == TypeItem.FERTILIZER)
        {
            ResourceManager.Instance.Fertilizer(Random.Range(min, max));
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerControl>().currentItem = this;
            interactionPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerControl>().currentItem = null;
            interactionPopup.SetActive(false);
        }
    }
}

public enum TypeItem
{ 
    WATER,
    FERTILIZER,
    SCRAPS,
    BATTERY
}

