using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tub : MonoBehaviour, IInteractable
{
    Animator anims;
    public AudioSource suond;

    public AudioClip boiling;
    public AudioClip newWig;

    bool boilFinished;

    private void Awake()
    {
        suond = GetComponent<AudioSource>();
        anims = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!suond.isPlaying && !boilFinished)
        {
            boilFinished = true;
            anims.SetTrigger("NewWig");
            suond.clip = newWig;
            suond.Play();
        }
    }

    public void EndInteraction()
    {

    }

    public void Interact()
    {
        StaticStuff.player.GrabbedBrosky.SetActive(false);
        StaticStuff.player.ChokeholdArm.SetActive(false);
        StaticStuff.player.NormalArm.SetActive(true);

        StoryEvents.instance.DunkBrosky();
    }

    public void Boil()
    {
        suond.Play();
    }
}
