using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlaySound();
    }

    public void PlaySound()
    {
        if (sound.isPlaying) return;
        sound.Play();
    }
}
