using UnityEngine;

public class Customer : MonoBehaviour, IInteractable
{
    bool interacting;
    bool startedStory;
    InkHandler inky;
    
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
        inky.onStoryEnded += EndInteraction;
    }

    void Update()
    {
        if (StaticStuff.isReadyForDialogue && interacting && !startedStory)
        {
            startedStory = true;
            inky.StartStory();
        }
    }
}
