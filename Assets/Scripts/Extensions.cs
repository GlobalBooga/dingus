using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {
    public static void FullDisable(this CanvasGroup canvas) {
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }

    public static void FullEnable(this CanvasGroup canvas) {
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }
}
