using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerBase : MonoBehaviour
{
    public GameObject interactionPopup;

    public GameObject towerLocked;
    public GameObject towerUnlocked;

    public TextMeshProUGUI textBuild;

    public int scraps;
    public int battery;

    public bool isUnlocked;

    private bool playerInside;
    void Start()
    {
        towerLocked.SetActive(true);
        towerUnlocked.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInside)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                UnlockTower();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUnlocked) return;
        if (collision.gameObject.tag.Equals("Player"))
        {
            textBuild.text = $"Build\nScraps: {scraps}\nBattery: {battery}";
            interactionPopup.SetActive(true);
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isUnlocked) return;
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionPopup.SetActive(false);
            playerInside = false;
        }
    }

    public void UnlockTower()
    {
        if(ResourceManager.Instance.scraps >= this.scraps && ResourceManager.Instance.battery >= this.battery)
        {
            ResourceManager.Instance.Scraps(-scraps);
            ResourceManager.Instance.Battery(-battery);

            towerLocked.SetActive(false);
            towerUnlocked.SetActive(true);

            interactionPopup.SetActive(false);

            playerInside = false;
        }
    }
}
