using System;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public static Fade instance;

    public CanvasGroup canvasGroup;

    private void Start()
    {
        instance = this;

        canvasGroup.alpha = 1;
        FadeIn();
    }

    public void FadeIn()
    {
        canvasGroup.LeanAlpha(0, 0.2f).setOnComplete(() => gameObject.SetActive(false));
    }

    public void FadeOut(Action action)
    {
        gameObject.SetActive(true);

        canvasGroup.LeanAlpha(1, 0.2f).setOnComplete(action);
    }
}
