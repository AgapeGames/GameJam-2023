using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public int water;
    public int fertilizer;

    public int scraps;
    public int battery;

    public int leaf;
    public int apple;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Water(int water)
    {
        this.water += water;
        CanvasManager.Instance.RefreshResource();
    }
    public void Fertilizer(int fertilizer)
    {
        this.fertilizer += fertilizer;
        CanvasManager.Instance.RefreshResource();
    }
    public void Scraps(int scraps)
    {
        this.scraps += scraps;
        CanvasManager.Instance.RefreshResource();
    }
    public void Battery(int battery)
    {
        this.battery += battery;
        CanvasManager.Instance.RefreshResource();
    }
    public void Leaf(int leaf)
    {
        this.leaf += leaf;
        CanvasManager.Instance.RefreshResource();
    }
    public void Apple(int apple)
    {
        this.apple += apple;
        CanvasManager.Instance.RefreshResource();
    }
}
