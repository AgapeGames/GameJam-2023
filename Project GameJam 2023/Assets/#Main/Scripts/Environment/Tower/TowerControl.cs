using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerControl : MonoBehaviour
{
    public GameObject interactionPopup;

    public float duration;
    public float counterDuration;

    public bool isActive;

    public float powerTarget;
    public float powerIncrement;
    public float powerCounter;

    public Slider sliderPower;

    private void Start()
    {
        Activated();
    }
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
                Deactivated();
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                powerCounter += powerIncrement;
            }
            if (powerCounter >= 0)
            {
                powerCounter -= Time.deltaTime * 2;
            }

            sliderPower.value = powerCounter;

            if (powerCounter >= powerTarget)
            {
                powerCounter = 0;
                sliderPower.value = powerCounter;
                Activated();
            }
        }
    }

    public void Deactivated()
    {
        powerCounter = 0;
        sliderPower.value = powerCounter;
        isActive = false;
    }

    public void Activated()
    {
        isActive = true;
        counterDuration = duration;
        interactionPopup.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive) return;
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionPopup.SetActive(false);
            if(isActive == false)
            {
                powerCounter = 0;
                sliderPower.value = powerCounter;
            }
        }
    }
}
