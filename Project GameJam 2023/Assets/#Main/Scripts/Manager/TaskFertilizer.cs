using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskFertilizer : MonoBehaviour
{
    public TaskControl taskControl;
    public GameObject panel;
    public ClickFertilizer[] listFertilizer;

    public int target;
    public int targetCounter;

    public float timer;
    public float timerCounter;
    public bool isPlay;

    public GameObject panelInfo;
    public GameObject panelLose;

    public TextMeshProUGUI textTimer;
    public TextMeshProUGUI textItem;
    private void OnEnable()
    {
        panelInfo.SetActive(true);
        panelLose.SetActive(false);
    }

    public void Play()
    {
        isPlay = true;
        timerCounter = timer;
        targetCounter = 0;
        panelInfo.SetActive(false);
        SpawnRandomImage();
    }
    private void Update()
    {
        if(isPlay)
        {
            if(timerCounter > 0)
            {
                timerCounter -= Time.deltaTime;
            }
            else
            {
                timerCounter = 0;
                Lose();
            }
        }

        textItem.text = "Item : " + $"{targetCounter}/{target}";
        textTimer.text = "Timer : " + (int)timerCounter;
    }
    void SpawnRandomImage()
    {
        for (int i = 0; i < listFertilizer.Length; i++)
        {
            Vector2 panelSize = panel.GetComponent<RectTransform>().sizeDelta;
            Vector2 randomPosition = new Vector2(Random.Range(-panelSize.x / 2, panelSize.x / 2), Random.Range(-panelSize.y / 2, panelSize.y / 2));
            listFertilizer[i].GetComponent<RectTransform>().anchoredPosition = randomPosition;
            listFertilizer[i].gameObject.SetActive(true);
        }

    }

    public void IncrementTarget()
    {
        targetCounter++;
        if(targetCounter>= target)
        {
            Win();
        }
    }

    public void Win()
    {
        isPlay = false;
        //taskControl.isUnlocked = true;
    }
    public void Lose()
    {
        isPlay = false;
        panelLose.gameObject.SetActive(true);
    }

    public void Close()
    {
        taskControl.ResetTask();
    }
}
