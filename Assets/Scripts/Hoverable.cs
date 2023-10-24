using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverable : MonoBehaviour {
    RectTransform rect;
    int ID;
    private void Awake() {
        rect = GetComponent<RectTransform>();
        ID = GetInstanceID();
    }

    public void Hover() {
        DOTween.Kill(ID);
        rect.DOScale(1.2f, .4f).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void Click() {
        DOTween.Kill(ID);
        rect.DOScale(1.4f, .4f).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void Unhover() {
        DOTween.Kill(ID);
        rect.DOScale(1, .4f).SetEase(Ease.OutBack).SetUpdate(true);
    }
}
