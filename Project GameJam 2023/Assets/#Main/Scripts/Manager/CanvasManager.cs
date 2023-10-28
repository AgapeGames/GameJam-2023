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


    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textNeedWater;
    public TextMeshProUGUI textNeedFertilizer;

    public TextMeshProUGUI textTimerWave;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        RefreshResource();
        RefreshRequirement();
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
    }

    public void RefreshRequirement()
    {
        textLevel.text = $"Tree Level\n{TreeControl.Instance.level}";
        textNeedWater.text = $"{TreeControl.Instance.currentWater}/{TreeControl.Instance.GetNeedWater()}";
        textNeedFertilizer.text = $"{TreeControl.Instance.currentFertilizer}/{TreeControl.Instance.GetNeedFertilizer()}";
    }
}
