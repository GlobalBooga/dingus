using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioClip mainFloor;
    [SerializeField] AudioClip basement;
    AudioSource sound;
    AudioSource basementMusic;
    public int index;

    private void Awake()
    {
        sound = GameObject.Find("Music").GetComponent<AudioSource>();
        basementMusic = GameObject.Find("MusicBasement").GetComponent<AudioSource>();
    }

    public void SetBasement()
    {
        sound.clip = basement;
        sound.Play();

        basementMusic.Play();
    }

    public void SetMainFloor()
    {
        sound.clip = mainFloor;
        sound.Play();
        basementMusic.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (index == 0 && sound.clip != mainFloor) SetMainFloor();
        if (index == 1 && sound.clip != basement) SetBasement();
    }
}
