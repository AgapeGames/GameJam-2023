using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFertilizer : MonoBehaviour
{
    public TaskControl taskControl;
    public GameObject panel;
    public ClickFertilizer[] listFertilizer;

    public int target;
    public int targetCounter;

    public GameObject panelInfo;
    public GameObject panelLose;
    private void OnEnable()
    {
        panelInfo.SetActive(true);
        panelLose.SetActive(false);
    }

    public void Play()
    {
        targetCounter = 0;
        panelInfo.SetActive(false);
        SpawnRandomImage();
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
        taskControl.GiveReward();
    }
    public void Lose()
    {
        panelLose.gameObject.SetActive(true);
    }

    public void Close()
    {
        taskControl.ResetTask();
    }
}
