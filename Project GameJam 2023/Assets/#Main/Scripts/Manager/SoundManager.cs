using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource audioSFX;
    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        
    }
    public void PlaySFX(AudioClip clip)
    {
        audioSFX.PlayOneShot(clip);
    }

    void Update()
    {
        
    }
}
