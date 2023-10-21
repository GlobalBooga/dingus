using Ink.Runtime;
using UnityEngine;

public class InkHandler : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    void Start()
    {
        story = new Story(inkJSON.text); 
        Debug.Log(loadStoryChunk());

        foreach (var item in story.currentChoices)
        {
            Debug.Log(item.text);
        }
    }

    string loadStoryChunk()
    {
        string text = "";

        if (story.canContinue)
        {

            text = story.ContinueMaximally();
        }

        return text;
    }
}
