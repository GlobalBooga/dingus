using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobUpAndDown : MonoBehaviour {
    RectTransform rect;
    private void Awake() {
        rect = GetComponent<RectTransform>();
    }
    private void Start() {
        rect.DOAnchorPosY(184, 1.2f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }
}
