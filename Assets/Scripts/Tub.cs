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
    bool boilStarted;

    bool interacted;

    private void Awake()
    {
        suond = GetComponent<AudioSource>();
        anims = GetComponent<Animator>();
    }

    private void Update()
    {
        if (boilStarted && !suond.isPlaying && !boilFinished)
        {
            boilFinished = true;
            anims.SetTrigger("NewWig");
            suond.clip = newWig;
            suond.Play();

            StoryEvents.instance.objective.text = "Take the wig";
        }
    }

    public void EndInteraction()
    {

    }

    public void Interact()
    {
        if (interacted) 
        { 
            if (suond.clip == newWig && !suond.isPlaying)
            {
                anims.SetTrigger("TakeWig");
                StoryEvents.instance.WigTaken();
                enabled = false;
            }
            return; 
        }
        interacted = true;
        StaticStuff.player.GrabbedBrosky.SetActive(false);
        StaticStuff.player.ChokeholdArm.SetActive(false);
        StaticStuff.player.NormalArm.SetActive(true);

        StoryEvents.instance.DunkBrosky();
    }

    public void Boil()
    {
        suond.Play();
        boilStarted = true;
    }
}
