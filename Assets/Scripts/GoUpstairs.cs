using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUpstairs : MonoBehaviour
{
    bool requestingTP;

    private void Start()
    {
        StaticStuff.transitionImage.onTransitionComplete +=OnTransistionCompleted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            requestingTP = true;
            StaticStuff.transitionImage.StartTransition(0,1);
        }
    }

    void OnTransistionCompleted()
    {
        if (!requestingTP) return;
        requestingTP = false;

        StaticStuff.player.transform.position = StaticStuff.goDownstairsTrigger.position - StaticStuff.goDownstairsTrigger.right;
<<<<<<< HEAD
        StaticStuff.player.SetLookRotation(-StaticStuff.goUpstairsTrigger.right);
        StaticStuff.minimapCamera.position = new Vector3(StaticStuff.minimapCamera.position.x, -10f, StaticStuff.minimapCamera.position.z);
=======
        StaticStuff.player.transform.rotation.SetLookRotation(StaticStuff.goDownstairsTrigger.right);
        StaticStuff.minimapCamera.position -= Vector3.down * 10;
>>>>>>> parent of bbb867b (prototda)
        StaticStuff.transitionImage.StartTransition(1, 0);
    }
}
