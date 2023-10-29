using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInfo : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelInfo;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            panelMain.SetActive(false);
            panelInfo.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            panelMain.SetActive(true);
            panelInfo.SetActive(false);
        }
    }
}
