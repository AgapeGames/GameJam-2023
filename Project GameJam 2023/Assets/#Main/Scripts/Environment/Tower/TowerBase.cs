using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public GameObject interactionPopup;

    public GameObject towerLocked;
    public GameObject towerUnlocked;

    public int scraps;
    public int battery;

    void Start()
    {
        towerLocked.SetActive(true);
        towerUnlocked.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void UnlockTower(int scraps, int battery)
    {
        if(scraps >= this.scraps && battery >= this.battery)
        {
            //Kurangin
            towerLocked.SetActive(false);
            towerUnlocked.SetActive(true);
        }
    }
}
