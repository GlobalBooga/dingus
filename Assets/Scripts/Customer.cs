using UnityEngine;

// old man
public class Customer : MonoBehaviour, IInteractable
{
    bool interacting;
    bool startedStory;
    InkHandler inky;
    bool waitingForWig;

    public bool pauseStory;

    public GameObject wig;

    Animator anims;
    public AudioClip[] footsteps;


    public void EndInteraction()
    {
        StaticStuff.instance.HideDialogueBox();
        
        interacting = false;
        if (!pauseStory)
        {
            startedStory = false;
        }

        // resets the buttons in case we were making a choice
        StaticStuff.ResetButtonLayoutGroup();

        StaticStuff.input.General.Look.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Interact()
    {
        if (pauseStory)
        {
            pauseStory = false;
            StaticStuff.buttonLayoutGroup.SetActive(true);
        }

        // all parts
        if (interacting)
        {
            inky.ProgressStory();
            return;
        }

        interacting = true;
        StaticStuff.instance.ShowDialogueBox();

        StaticStuff.input.General.Look.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    private void Awake()
    {
        inky = GetComponent<InkHandler>();
        inky.onStoryEnded += ()=> { EndInteraction(); waitingForWig = true;};
        anims = GetComponent<Animator>();
    }

    void Update()
    {
        if (StaticStuff.isReadyForDialogue && interacting)
        {
            // part 1
            if (!startedStory)
            {
                startedStory = true;
                inky.StartStory(0);
            }

            // part 2
            else if (waitingForWig)
            {
                if (!StoryEvents.gotWig) return;

                StoryEvents.instance.objective.text = "";

                waitingForWig = false;
                inky.StartStory(1);
                inky.ProgressStory();
            }
        }
    }


    public void Sit()
    {
        anims.Play("Sit", 0);
    }

    public void Walk()
    {
        anims.Play("Walk", 0);
    }

    public void Idle()
    {
        anims.Play("Idle",0);
    }

    public void GoSit()
    {
        anims.SetTrigger("GoSit");
    }

    public void Leave()
    {
        inky.onStoryEnded += () =>
        {
            anims.SetTrigger("Leave");
        };
    }

    public void Enter()
    {
        anims.SetTrigger("Enter");    
    }

    public void GetGrabbed()
    {
        inky.onStoryEnded += () => 
        { 
            StaticStuff.player.GrabbedBrosky.SetActive(true);
            StaticStuff.player.ChokeholdArm.SetActive(true);
            StaticStuff.player.NormalArm.SetActive(false);
            gameObject.SetActive(false);
        };
    }

    public void GetDunked()
    {
        anims.SetTrigger("Die");
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    public void PlayBoilingSound()
    {
        StaticStuff.tub.Boil();
    }
}
