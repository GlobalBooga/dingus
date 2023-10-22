using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomBlocker : MonoBehaviour
{
    BoxCollider bc;

    private void Awake()
    {
        bc = GetComponent<BoxCollider>();
    }

    public void SetBlocked()
    {
        bc.enabled = true;
    }

    public void SetUnblocked()
    {
        bc.enabled = false;
    }
}
