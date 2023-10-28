using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskControl : MonoBehaviour
{
    public GameObject interactionPopup;

    public TaskType taskType;
    public GameObject prefabWater;
    public GameObject prefabFertilizer;

    public Transform posSpawn;

    public bool isActive;
    public float duration;
    public float durationCounter;

    public bool playerInside;
    void Start()
    {
        
    }

    void Update()
    {
        if (!isActive)
        {
            durationCounter -= Time.deltaTime;

            if (durationCounter <= 0)
            {
                isActive = true;
            }
        }
        else
        {
            if (playerInside)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (taskType == TaskType.WATER)
                    {

                    }
                    else if (taskType == TaskType.FERTILIZER)
                    {
                        //Open Canvas Mini Game
                    }


                    interactionPopup.SetActive(false);
                    //Contoh
                    GiveReward();
                }
            }
        }
    }

    public void GiveReward()
    {
        interactionPopup.SetActive(false);
        isActive = false;
        durationCounter = duration;

        if (taskType == TaskType.WATER)
        {
            Instantiate(prefabWater, posSpawn.position, posSpawn.rotation);
        }
        else if (taskType == TaskType.FERTILIZER)
        {
            Instantiate(prefabFertilizer, posSpawn.position, posSpawn.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionPopup.SetActive(true);
            playerInside = true;
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
}


public enum TaskType
{
    WATER,
    FERTILIZER
}