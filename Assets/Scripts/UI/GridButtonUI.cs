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
        levelLoader.LoadLevel(index);
    }

    public void SetUpButton(LevelLoader levelLoader, int index, bool unlocked)
    {
        this.levelLoader = levelLoader;
        this.index = index;
        label.text = (index + 1).ToString();
        if (!unlocked)
            button.interactable = false;
    }
}
