using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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

    public Slider sliderDuration;

    [Header("Fertilizer")]
    public int minLeaf, maxLeaf;
    private int needLeaf;

    public TextMeshProUGUI textNeedLeaf;
    void Start()
    {
        needLeaf = Random.Range(minLeaf, maxLeaf);
    }

    void Update()
    {
        if (!isActive)
        {
            durationCounter -= Time.deltaTime;

            sliderDuration.gameObject.SetActive(true);
            sliderDuration.value = durationCounter / duration;
            if (durationCounter <= 0)
            {
                isActive = true;
            }
        }
        else
        {
            sliderDuration.gameObject.SetActive(false);
            if (playerInside)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (taskType == TaskType.WATER)
                    {
                        GameManager.Instance.PlayerFreeze();
                        CanvasManager.Instance.panelTaskWater.SetActive(true);
                        interactionPopup.SetActive(false);
                    }
                    else if (taskType == TaskType.FERTILIZER)
                    {
                        GenerateFertilizer();
                    }


                    //Contoh
                }
            }
        }
    }

    public void GenerateFertilizer()
    {
        if(ResourceManager.Instance.leaf >= needLeaf)
        {
            ResourceManager.Instance.Leaf(-needLeaf);

            needLeaf = Random.Range(minLeaf, maxLeaf);

            textNeedLeaf.text = "" + needLeaf;
            GiveReward();
        }
    }

    public void ResetTask()
    {
        GameManager.Instance.PlayerUnfreeze();
        CanvasManager.Instance.panelTaskWater.SetActive(false);
        CanvasManager.Instance.panelTaskFertilizer.SetActive(false);
        interactionPopup.SetActive(false);
        isActive = false;
        durationCounter = duration;
    }

    public void GiveReward()
    {
        ResetTask();

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
            if(taskType == TaskType.FERTILIZER)
            {
                textNeedLeaf.text = "" + needLeaf;
            }

                
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