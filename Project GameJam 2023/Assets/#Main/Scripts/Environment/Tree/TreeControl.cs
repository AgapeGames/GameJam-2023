using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeControl : MonoBehaviour
{
    public static TreeControl Instance;

    public int health;
    public int level;

    public int currentWater;
    public int[] requirementWater;
    public int currentFertilizer;
    public int[] requirementFertilizer;

    public GameObject interactionPopup;

    public bool playerInside;


    public GameObject[] listSkin;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(playerInside)
        {
            if (currentFertilizer == GetNeedFertilizer() && currentWater == GetNeedWater())
            {
                currentFertilizer = 0;
                currentWater = 0;
                level++;

                RefreshSize();
            }

            if (Input.GetKey(KeyCode.E))
            {

                if (currentWater < GetNeedWater())
                {
                    if (ResourceManager.Instance.water > 0)
                    {
                        ResourceManager.Instance.Water(-1);
                        currentWater++;
                    }
                }

                if (currentFertilizer < GetNeedFertilizer())
                {
                    if (ResourceManager.Instance.fertilizer > 0)
                    {
                        ResourceManager.Instance.Fertilizer(-1);
                        currentFertilizer++;
                    }
                }

                CanvasManager.Instance.RefreshRequirement();
            }
        }

    }

    public void RefreshSize()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == level - 1)
            {
                listSkin[i].SetActive(true);
            }
            else
            {
                listSkin[i].SetActive(false);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionPopup.SetActive(true);
            playerInside = true;
            CanvasManager.Instance.RefreshRequirement();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionPopup.SetActive(false);
            playerInside = false;
        }
    }

    public int GetNeedWater()
    {
        return requirementWater[level - 1];
    }
    public int GetNeedFertilizer()
    {
        return requirementFertilizer[level - 1];
    }

}
