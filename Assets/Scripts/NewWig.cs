using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWig : MonoBehaviour
{
    public GameObject wig2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wig2.SetActive(true);
            StoryEvents.instance.WigStored();
        }
    }
}
