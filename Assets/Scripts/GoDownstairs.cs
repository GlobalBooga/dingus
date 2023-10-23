using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDownstairs : MonoBehaviour
{
    bool requestingTP;

    private void Start()
    {
        StaticStuff.transitionImage.onTransitionComplete += OnTransistionCompleted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            requestingTP = true;
            StaticStuff.transitionImage.StartTransition(0, 1);
        }
    }

    void OnTransistionCompleted()
    {
        if (!requestingTP) return;
        requestingTP = false;

        StaticStuff.player.transform.position = StaticStuff.goUpstairsTrigger.position - StaticStuff.goUpstairsTrigger.right;
        StaticStuff.player.SetLookRotation(-StaticStuff.goUpstairsTrigger.right);
        StaticStuff.minimapCamera.position = new Vector3(StaticStuff.minimapCamera.position.x, -20f, StaticStuff.minimapCamera.position.z);
        StaticStuff.transitionImage.StartTransition(1,0);
    }
}
