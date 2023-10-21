using System.Collections;
using UnityEngine;

public class StaticStuff : MonoBehaviour
{
    public static StaticStuff instance;
    public static GameObject minimap;
    public static Player player;


    [Header("DIALOGUE BOX")]
    [SerializeField] float speed = 4;
    RectTransform dialogueBox;
    Coroutine currentDialogueRoutine;
    AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    Vector3 shown, hidden;


    // interact prompt
    GameObject interactPrompt;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        minimap = GameObject.Find("Minimap");
        player = GameObject.Find("Player").GetComponent<Player>();

        dialogueBox = GameObject.Find("DialogueBox").transform as RectTransform;
        shown = new Vector3(dialogueBox.position.x, 0, dialogueBox.position.z);
        hidden = new Vector3(dialogueBox.position.x, -dialogueBox.rect.height, dialogueBox.position.z);
        SetDialogueHidden();

        interactPrompt = GameObject.Find("InteractPrompt");
        HideInteractPrompt();
    }

    public void ShowDialogueBox()
    {
        if (currentDialogueRoutine != null) StopCoroutine(currentDialogueRoutine);
        currentDialogueRoutine = StartCoroutine(DialogueRoutine(true));
    }
    
    public void HideDialogueBox()
    {
        if (currentDialogueRoutine != null) StopCoroutine(currentDialogueRoutine);
        currentDialogueRoutine = StartCoroutine(DialogueRoutine(false));
    }

    IEnumerator DialogueRoutine(bool show)
    {
        Vector3 start = dialogueBox.position;
        Vector3 end = show == true ? shown : hidden; 

        for (float i = 0; i < 1; i += Time.deltaTime * speed) 
        {
            dialogueBox.position = Vector3.Lerp(start, end, curve.Evaluate(i));
            yield return null;
        }

        if (show) SetDialogueShown();
        else SetDialogueHidden();

        currentDialogueRoutine = null;
    }

    public void SetDialogueShown()
    {
        dialogueBox.position = shown;
    }

    public void SetDialogueHidden()
    {
        dialogueBox.position = hidden;
    }


    public void ShowInteractPrompt()
    {
        interactPrompt.SetActive(true);
    }

    public void HideInteractPrompt()
    {
        interactPrompt.SetActive(false);
    }
}

