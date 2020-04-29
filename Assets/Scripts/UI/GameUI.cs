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
        this.time = time;
        currTime = time;
        clockText.text = time.ToString();
        enabled = true;
    }

    private void OnClockUpdate()
    {
        currTime--;

        float from = clockFill.fillAmount;
        float to = currTime / (float)time;

        LeanTween.value(from, to, 1)
            .setOnUpdate(fill => clockFill.fillAmount = fill)
            .setOnComplete(() => clockText.text = currTime.ToString());
    }

    private void OnTimesUp()
    {
        FadeIn(timesUp);
    }

    private void OnComplete()
    {
        if (!levelLoader.IsNextLevelAvailable())
            nextLevel.interactable = false;

        FadeIn(complete);
    }

    private void FadeIn(GameObject gameObject)
    {
        CanvasGroup canvas = gameObject.GetComponent<CanvasGroup>();

        var seq = LeanTween.sequence();
        seq.append(1);
        seq.append(() => {
            canvas.alpha = 0;
            gameObject.SetActive(true);
        });
        seq.append(() => canvas.LeanAlpha(1, 0.1f));
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
