using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionImage : MonoBehaviour
{
    Image image;
    public float speed;
    public Action onTransitionComplete;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void StartTransition(float startAlpha, float endAlpha)
    {
        StartCoroutine(TransitionRoutine(startAlpha, endAlpha));
    }

    IEnumerator TransitionRoutine(float startAlpha, float endAlpha)
    {
        for (float i = 0; i < 1; i += Time.deltaTime * speed)
        {
            image.color = new Color(0, 0, 0, Mathf.Lerp(startAlpha, endAlpha, i));
            yield return null;
        }

        onTransitionComplete?.Invoke();
    }
}
