using System;
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
        gameObject.SetActive(false);
    }
}
