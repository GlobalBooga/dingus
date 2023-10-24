using System.Collections;
using TMPro;
using UnityEngine;

public class StoryEvents : MonoBehaviour
{
    public static StoryEvents instance;

    const string getWigEvent = "GoGetWig\n";
    const string badStylingEvent = "BadWigStyling\n";
    const string goodStylingEvent = "GoodWigStyling\n";
    const string oldManLeaveEvent = "OldManLeave\n";
    const string youngmanSitEvent = "GoSit\n";
    const string killEvent = "Kill\n";
    public GameObject wigPrefab;
    public Wig wig;
    public GameObject[] wigPath;

    public TextMeshProUGUI objective;

    public static bool gotWig;

    public InkHandler[] inkRefs;
    public Customer[] customers;

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
        else if (eventName == goodStylingEvent) StartCoroutine(WigStyling());
        else if (eventName == badStylingEvent) StartCoroutine(WigStyling());
        else if (eventName == oldManLeaveEvent) OldManLeave();
        else if (eventName == killEvent) Kill();
        else if (eventName == youngmanSitEvent) YoungmanSit();
    }

    public void GoGetWig()
    {
        objective.text = "Go get a wig";

        customers[0].GoSit();

        foreach (var t in wigPath)
        {
            t.SetActive(true);
        }
    }


    public void OldManLeave()
    {
        customers[0].Leave();

        Invoke(nameof(YoungmanEnter), 8);
    }

    void YoungmanEnter()
    {

        customers[1].Enter();
    }

    void YoungmanSit()
    {
        StaticStuff.instance.HideDialogueBox();
        customers[1].GoSit();
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
        inkRefs[0].GetComponent<Customer>().wig.SetActive(true);

        StaticStuff.transitionImage.StartTransition(1, 0);


        StaticStuff.buttonLayoutGroup.SetActive(true);
        StaticStuff.instance.ShowDialogueBox();
    }

    public void Kill()
    {
        customers[1].EndInteraction();

    }
}
