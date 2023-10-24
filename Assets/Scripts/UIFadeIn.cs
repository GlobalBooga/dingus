using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeIn : MonoBehaviour
{
    CanvasGroup canvas;
    [SerializeField] float fadeTo;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.DOFade(fadeTo, .8f).SetDelay(.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
