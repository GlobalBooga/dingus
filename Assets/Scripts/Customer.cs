using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour, IInteractable
{
    bool interacting;
    
    public void EndInteraction()
    {
        StaticStuff.instance.HideDialogueBox();
    }

    public void Interact()
    {
        if (!interacting)
        {
            interacting = true;
            StaticStuff.instance.ShowDialogueBox();
        }
        else
        {
            interacting = false;
            StaticStuff.instance.HideDialogueBox();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
