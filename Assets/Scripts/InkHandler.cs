using Ink.Runtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InkHandler : MonoBehaviour
{
    public TextAsset inkJSON;
    public Story story { get; private set; }
    public Action onStoryEnded;

    float btnHeight;

    public void StartStory()
    {
        story = new Story(inkJSON.text);
        ProgressStory();
    }

    public void ProgressStory()
    {
        if (StaticStuff.isPrintingDialogue)
        {
            StaticStuff.instance.PrintDialogue(story.currentText);
            return;
        }

        string text = "";

        if (story.canContinue)
        {
            text = story.Continue();
        }
        else
        {
            if (story.currentChoices.Count > 0)
            {
                foreach (var item in story.currentChoices)
                {
                    RectTransform btn = Instantiate(StaticStuff.instance.choiceButtonPrefab, StaticStuff.buttonLayoutGroup.transform).transform as RectTransform;
                    if (btnHeight == 0) btnHeight = btn.rect.height + 5;
                    StaticStuff.buttonLayoutGroup.transform.position += Vector3.up * btnHeight/2;
                    btn.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        MakeChoice(item.index);
                    });

                    btn.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.text;

                }
                StaticStuff.input.General.Interact.Disable();
                return;
            }
          
            //done
            StaticStuff.instance.HideDialogueBox();
            onStoryEnded?.Invoke();
        }

        StaticStuff.instance.PrintDialogue(text);
    }


    public void MakeChoice(int choiceIndex)
    {
        // make choice
        story.ChooseChoiceIndex(choiceIndex);

        StaticStuff.ResetButtonLayoutGroup();

        // continue
        story.Continue();
        ProgressStory();
    }
    
}
