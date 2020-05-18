using UnityEngine;
using UnityEngine.UI;

public class GridButtonUI : MonoBehaviour
{
    private LevelLoader levelLoader;

    public Button button;
    public Text label;

    [Space]
    public Image locked;

    [Space]
    public Image[] stars;

    [Space]
    public Color starColor;

    private int index;

    public void EnterLevel()
    {
        AudioManager.Play(SFX.Click);

        Fade.instance.FadeOut(() => levelLoader.LoadLevel(index));
    }

    public void SetUpButton(LevelLoader levelLoader, int index, LevelData.Level level)
    {
        if (level.unlocked)
        {
            this.levelLoader = levelLoader;
            this.index = index;
            label.text = (index + 1).ToString();

            for (int i = 0; i < level.rating; i++)
            {
                stars[i].color = starColor;
            }
        }
        else
        {
            button.interactable = false;
            
            label.gameObject.SetActive(false);
            locked.gameObject.SetActive(true);

            stars[0].transform.parent.gameObject.SetActive(false);
        }
    }
}
