using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    public TextMeshProUGUI textBattery;
    public TextMeshProUGUI textScraps;
    public TextMeshProUGUI textWater;
    public TextMeshProUGUI textFertilizer;
    public TextMeshProUGUI textLeaf;
    public TextMeshProUGUI textApple;


    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textNeedWater;
    public TextMeshProUGUI textNeedFertilizer;

    public TextMeshProUGUI textTimerWave;


    public GameObject panelTaskWater;
    public GameObject panelTaskFertilizer;

    public PanelSkill panelSkill;
    public GameObject panelLose;
    public GameObject panelWin;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        RefreshResource();
    }

    void Update()
    {
        
    }

    public void RefreshResource()
    {
        textWater.text = $"{ResourceManager.Instance.water}";
        textFertilizer.text = $"{ResourceManager.Instance.fertilizer}";
        textScraps.text = $"{ResourceManager.Instance.scraps}";
        textBattery.text = $"{ResourceManager.Instance.battery}";
        textLeaf.text = $"{ResourceManager.Instance.leaf}";
        textApple.text = $"{ResourceManager.Instance.apple}";
        textLevel.text = $"Tree Level : {TreeControl.Instance.level}";
    }

    public void PanelSkill(bool con)
    {
        panelSkill.gameObject.SetActive(con);
        if (con)
        {
            panelSkill.RefreshText();
            GameManager.Instance.PlayerFreeze();
        }
        else
        {
            GameManager.Instance.PlayerUnfreeze();
        }
    }
}
