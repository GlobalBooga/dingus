using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wig : MonoBehaviour, IInteractable
{
    public Action onInteracted;

    public void EndInteraction()
    {
       
    }

    public void Interact()
    {
        onInteracted?.Invoke();
        Destroy(gameObject);
    }
}
