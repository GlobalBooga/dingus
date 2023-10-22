using System.Collections;
using TMPro;
using UnityEngine;

public class StoryEvents : MonoBehaviour
{
    public static StoryEvents instance;

    const string getWigEvent = "GoGetWig\n";
    const string badStylingEvent = "BadWigStyling\n";
    const string goodStylingEvent = "GoodWigStyling\n";
    public GameObject wigPrefab;
    public Wig wig;
    public GameObject[] wigPath;

    public TextMeshProUGUI objective;

    public static bool gotWig;

    InkHandler currentInkRef;

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

    public void CallEvent(string eventName, InkHandler sender)
    {
        currentInkRef = sender;
        if (eventName == getWigEvent) GoGetWig();
        else if (eventName == goodStylingEvent) StartCoroutine(WigStyling());
        else if (eventName == badStylingEvent) StartCoroutine(WigStyling());
    }

    public void GoGetWig()
    {
        objective.text = "Go get a wig";

        foreach (var t in wigPath)
        {
            t.SetActive(true);
        }
    }


    IEnumerator WigStyling()
    {
        StaticStuff.instance.HideDialogueBox();
        StaticStuff.ResetButtonLayoutGroup();
        StaticStuff.buttonLayoutGroup.SetActive(false);

        while (StaticStuff.isReadyForDialogue)
        {
            yield return null;
        }

        StaticStuff.player.PlayAnimation("CutHair");

        yield return new WaitForSeconds(1f);

        StaticStuff.transitionImage.StartTransition(0, 1);

        yield return new WaitForSeconds(1);
        currentInkRef.GetComponent<Customer>().wig.SetActive(true);

        StaticStuff.transitionImage.StartTransition(1, 0);


        StaticStuff.buttonLayoutGroup.SetActive(true);
        StaticStuff.instance.ShowDialogueBox();
    }
}
