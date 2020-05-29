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

    // Fades in screen
    public void FadeIn()
    {
        canvasGroup.LeanAlpha(0, 0.2f).setOnComplete(() => gameObject.SetActive(false));
    }

    // Fades out screen and executes action on complete
    public void FadeOut(Action action)
    {
        gameObject.SetActive(true);

        canvasGroup.LeanAlpha(1, 0.2f).setOnComplete(action);
    }
}
