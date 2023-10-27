using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    public float duration;
    public float counterDuration;

    public bool isActive;

    void Update()
    {
        if (isActive)
        {
            if (counterDuration > 0)
            {
                counterDuration -= Time.deltaTime;
            }
            else
            {
                isActive = false;
            }
        }
    }

    public void Activated()
    {
        isActive = true;
        counterDuration = duration;
    }
}
