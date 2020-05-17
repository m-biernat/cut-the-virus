using UnityEngine;
using UnityEngine.UI;

public class GridButtonUI : MonoBehaviour
{
    private LevelLoader levelLoader;

    public Button button;
    public Text label;

    private int index;

    public void EnterLevel()
    {
        AudioManager.Play(SFX.Click);

        Fade.instance.FadeOut(() => levelLoader.LoadLevel(index));
    }

    public void SetUpButton(LevelLoader levelLoader, int index, bool unlocked)
    {
        this.levelLoader = levelLoader;
        this.index = index;
        label.text = index.ToString();
        if (!unlocked)
            button.interactable = false;
    }
}
