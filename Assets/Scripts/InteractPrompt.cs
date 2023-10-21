using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPrompt : MonoBehaviour
{
    float time = 0;
    float delay = 0.75f;
    bool isE1 = true;

    Image img;
    [SerializeField] Sprite e1;
    [SerializeField] Sprite e2;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > delay)
        {
            time = 0;
            img.sprite = isE1 ? e1 : e2;
            isE1 = !isE1;
        }
    }
}
