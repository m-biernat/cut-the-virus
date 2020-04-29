using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image clockFill;
    public Text clockText;

    private int time, currTime;

    public GameObject timesUp, complete;

    public LevelLoader levelLoader;

    public Button nextLevel;
        
    void Start()
    {
        GameManager.instance.OnTick += OnClockUpdate;
        GameManager.instance.OnTimesUp += OnTimesUp;
        GameManager.instance.OnComplete += OnComplete;
    }

    public void Init(int time)
    {
        this.time = time + 1;
        currTime = time + 1;
        clockText.text = time.ToString();
        enabled = true;
    }

    private void OnClockUpdate()
    {
        currTime--;
        clockText.text = currTime.ToString();

        float from = clockFill.fillAmount;
        float to = currTime / (float)time;

        LeanTween.value(from, to, 1)
            .setOnUpdate(fill => clockFill.fillAmount = fill);
    }

    private void OnTimesUp()
    {
        timesUp.SetActive(true);
    }

    private void OnComplete()
    {
        if (!levelLoader.IsNextLevelAvailable())
            nextLevel.interactable = false;

        complete.SetActive(true);
    }

    public void MainMenu()
    {
        Fade.instance.FadeOut(() => levelLoader.LoadMenu(false));
    }

    public void Restart()
    {
        Fade.instance.FadeOut(() => levelLoader.ReloadLevel());
    }

    public void SelectLevel()
    {
        Fade.instance.FadeOut(() => levelLoader.LoadMenu(true));
    }

    public void NextLevel()
    {
        Fade.instance.FadeOut(() => levelLoader.LoadNextLevel());
    }
}
