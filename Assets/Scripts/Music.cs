using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioClip mainFloor;
    [SerializeField] AudioClip basement;
    AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();   
    }

    public void SetBasement()
    {
        sound.clip = basement;
        sound.Play();
    }

    public void SetMainFloor()
    {
        sound.clip = mainFloor;
        sound.Play();
    }
}
