using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomUnlock : MonoBehaviour
{
    [SerializeField] GameObject mapFog;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mapFog.SetActive(false);
        }
    }
}
