using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskWater : MonoBehaviour
{
    public TaskControl taskControl;
    public GameObject panelFail;

    public ObjectDragWater[] listObject;
    public Transform[] listPosition;

    public Image imageSoal;

    public Sprite[] listSprite;
    public int answer;
    public void Play()
    {
        Shuffle(listObject);
    }
    void Shuffle(ObjectDragWater[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            ObjectDragWater temp = array[i];
            int randomIndex = Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }

        listObject = array;
        SetUp();
    }
    public void SetUp()
    {
        answer = Random.Range(1, 4);
        imageSoal.sprite = listSprite[answer - 1];

        for (int i = 0; i < listObject.Length; i++)
        {
            listObject[i].gameObject.transform.position = listPosition[i].position;
        }
    }

    private void OnEnable()
    {
        Play();
        panelFail.SetActive(false);
    }
    public void Done(int val)
    {
        if(val == answer)
            taskControl.GiveReward();
        else
            panelFail.SetActive(true);
    }

    public void Close()
    {
        taskControl.ResetTask();
    }
    
}
