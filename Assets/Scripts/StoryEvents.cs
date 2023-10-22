using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryEvents : MonoBehaviour
{
    public static StoryEvents instance;

    const string getWigEvent = "GoGetWig\n";
    public GameObject wigPrefab;
    public Wig wig;
    public GameObject[] wigPath;

    public TextMeshProUGUI objective;

    public static bool gotWig;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

        wig.onInteracted += ()=>
        {
            objective.text = "Return to the customer";
            foreach (var t in wigPath)
            {
                t.SetActive(false);
            }

            gotWig = true;
        };
    }

    public void CallEvent(string eventName)
    {
        if (eventName == getWigEvent) GoGetWig();
    }

    public void GoGetWig()
    {
        objective.text = "Go get a wig";

        foreach (var t in wigPath)
        {
            t.SetActive(true);
        }
    }
}
