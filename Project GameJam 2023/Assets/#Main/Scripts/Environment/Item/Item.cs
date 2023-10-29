using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject interactionPopup;

    public TypeItem typeItem;
    public int min, max;

    public TextInfo textInfo;
    public Transform posSpawnText;
    public Transform objectCanvas;
    public bool isUsed = false;

    public GameObject objSprite;
    public CircleCollider2D collider;
    public GameObject effectCollect;

    public void GetItem()
    {
        if (isUsed) return;

        isUsed = true;
        int value = Random.Range(min, max);
       

        GameObject newTextInfo = Instantiate(textInfo.gameObject, posSpawnText.position, posSpawnText.rotation, objectCanvas.transform);
        TextInfo newInfo = newTextInfo.GetComponent<TextInfo>();
        newInfo.ShowText($"+{value} {typeItem.ToString()}", 1);
         if (typeItem == TypeItem.SCRAPS)
        {
            ResourceManager.Instance.Scraps(value);
        }else if (typeItem == TypeItem.BATTERY)
        {
            ResourceManager.Instance.Battery(value);
        }
        else if (typeItem == TypeItem.WATER)
        {
            ResourceManager.Instance.Water(value);
        }
        else if (typeItem == TypeItem.FERTILIZER)
        {
            ResourceManager.Instance.Fertilizer(value);
        }
        else if (typeItem == TypeItem.LEAF)
        {
            ResourceManager.Instance.Leaf(value);
        }
        else if (typeItem == TypeItem.APPLE)
        {
            ResourceManager.Instance.Apple(value);
        }

        interactionPopup.SetActive(false);
        objSprite.SetActive(false);
        collider.enabled = false;
        //EffectSpawn
        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerControl>().SetCurrentItem(this);
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
    BATTERY,
    APPLE,
    LEAF
}

