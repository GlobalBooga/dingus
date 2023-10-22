using UnityEngine;

// old man
public class Customer : MonoBehaviour, IInteractable
{
    bool interacting;
    bool startedStory;
    InkHandler inky;
    bool waitingForWig;

    public GameObject wig;
    
    public void EndInteraction()
    {
        StaticStuff.instance.HideDialogueBox();
        interacting = false;
        startedStory = false;

        // resets the buttons in case we were making a choice
        StaticStuff.ResetButtonLayoutGroup();

        StaticStuff.input.General.Look.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Interact()
    {
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
}
