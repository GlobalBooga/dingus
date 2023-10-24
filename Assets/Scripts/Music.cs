using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioClip mainFloor;
    [SerializeField] AudioClip basement;
    AudioSource sound;
    public int index;

    private void Awake()
    {
        sound = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    private void Start() {
        PauseManager.instance.PauseEvent += Pause;
        PauseManager.instance.VolumeChangedEvent += VolumeChanged;
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

    private void OnTriggerEnter(Collider other)
    {
        if (index == 0 && sound.clip != mainFloor) SetMainFloor();
        if (index == 1 && sound.clip != basement) SetBasement();
    }

    void Pause(bool paused) {
        sound.volume = paused ? 0 : PauseManager.audioVolume;
    }

    void VolumeChanged(float value) {
        sound.volume = PauseManager.Paused ? 0 : value;
    }
}
