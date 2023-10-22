using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvents : MonoBehaviour
{
    public void GoGetWig()
    {
        Debug.Log("GoGetWig");
    }

    public void CallEvent(string eventName)
    {
        Invoke(eventName, 0);
    }
}
