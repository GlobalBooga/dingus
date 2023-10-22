using System.Collections;
using TMPro;
using UnityEngine;

public class StaticStuff : MonoBehaviour
{
    public static StaticStuff instance;
    public static GameObject minimap;
    public static Transform minimapCamera;
    public static Player player;
    public static MyInput input;
    public static GameObject canvas;
    public static GameObject buttonLayoutGroup;
    public static SecretRoomBlocker secretRoomBlocker;

    public static Transform goUpstairsTrigger;
    public static Transform goDownstairsTrigger;
    public static TransitionImage transitionImage;


    public GameObject choiceButtonPrefab;
    static Vector3 buttonLayoutDefaultPos;

    [Header("DIALOGUE BOX")]
    [SerializeField] float speed = 4;
    [SerializeField] float printDelay = 0.25f;
    RectTransform dialogueBox;
    Coroutine currentDialogueRoutine, currentTextPrintingRoutine;
    AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    Vector3 shown, hidden;
    TextMeshProUGUI dialogueText;
    public static bool isReadyForDialogue {  get; private set; }
    public static bool isPrintingDialogue {  get; private set; }


    // interact prompt
    GameObject interactPrompt;


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


        input = new MyInput();

        transitionImage = GameObject.Find("TransitionImage").GetComponent<TransitionImage>();
        secretRoomBlocker = GameObject.Find("SecretRoomBlocker").GetComponent<SecretRoomBlocker>();

        buttonLayoutGroup = GameObject.Find("DialogueChoices");
        buttonLayoutDefaultPos = buttonLayoutGroup.transform.position;
        canvas = GameObject.Find("Canvas");
        minimap = GameObject.Find("Minimap");
        minimapCamera = GameObject.Find("minimapCamera").transform;
        player = GameObject.Find("Player").GetComponent<Player>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();

        dialogueBox = GameObject.Find("DialogueBox").transform as RectTransform;
        shown = new Vector3(dialogueBox.position.x, 0, dialogueBox.position.z);
        hidden = new Vector3(dialogueBox.position.x, -dialogueBox.rect.height, dialogueBox.position.z);
        SetDialogueHidden();

        interactPrompt = GameObject.Find("InteractPrompt");
        HideInteractPrompt();


        goUpstairsTrigger = GameObject.Find("GoUpstairs").transform;
        goDownstairsTrigger = GameObject.Find("GoDownstairs").transform;
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

        if (!show) dialogueText.text = "";

        for (float i = 0; i < 1; i += Time.deltaTime * speed) 
        {
            dialogueBox.position = Vector3.Lerp(start, end, curve.Evaluate(i));
            yield return null;
        }

        if (show) SetDialogueShown();
        else SetDialogueHidden();

        currentDialogueRoutine = null;

        isReadyForDialogue = show ? true : false;
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
    

    public void PrintDialogue(string text)
    {
        if (currentTextPrintingRoutine != null)
        {
            StopCoroutine(currentTextPrintingRoutine);
            dialogueText.text = text;
            currentTextPrintingRoutine = null;
            isPrintingDialogue = false;
            return;
        }

        isPrintingDialogue = true;
        currentTextPrintingRoutine = StartCoroutine(PrintDialogueRoutine(text));
    }

    IEnumerator PrintDialogueRoutine(string text)
    {
        dialogueText.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            dialogueText.text += text[i];
            yield return new WaitForSeconds(printDelay);
        }

        currentTextPrintingRoutine = null;
        isPrintingDialogue = false;
    }

    public static void ResetButtonLayoutGroup()
    {
        // delete other choices
        for (int i = 0; i < buttonLayoutGroup.transform.childCount; i++)
        {
            // make them fade away
            Destroy(buttonLayoutGroup.transform.GetChild(i).gameObject);
        }

        buttonLayoutGroup.transform.position = buttonLayoutDefaultPos;

        input.General.Interact.Enable();
    }
}

