using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image clockFill;
    public Text clockText;
    public Color clockTextColor;

    private int time;

    [Space]
    public GameObject timesUp;
    public GameObject complete;

    [Space]
    public LevelLoader levelLoader;

    public Button nextLevel;

    [Space]
    public Image[] stars;
    public Color starColor;    

    private int clockID;

    [Space]
    public Text countdown;
    private Vector3 countdownScale = new Vector3(1.25f, 1.25f);

    [Space]
    public Text starCount;

    void Start()
    {
        GameManager.instance.OnCountdown += OnCountdown;

        GameManager.instance.OnTick += OnClockUpdate;
        GameManager.instance.OnAllyKilled += UpdateStarCount;

        GameManager.instance.OnTimesUp += OnTimesUp;
        GameManager.instance.OnComplete += OnComplete;
    }

    public void Init(int time)
    {
        this.time = time;
        clockText.text = time.ToString();
        enabled = true;
        countdown.text = "";
    }

    private void OnClockUpdate()
    {
        int currTime = GameManager.timeLeft - 1;

        float from = clockFill.fillAmount;
        float to = currTime / (float)time;

        clockID = LeanTween.value(from, to, 1)
            .setOnUpdate(fill => clockFill.fillAmount = fill)
            .setOnComplete(() => {
                if (currTime == 0)
                    clockText.color = clockTextColor;
                clockText.text = currTime.ToString();
                })
            .id;

        if (currTime < 3)
            clockText.rectTransform
                .LeanScale(new Vector3(1.25f, 1.25f), 0.25f).setEaseInOutBounce()
                .setOnComplete(() => clockText.rectTransform.localScale = Vector3.one);

        AudioManager.Play(SFX.Tick);
    }

    private void OnCountdown()
    {
        if (GameManager.instance.countdown > 0)
        {
            countdown.rectTransform.localScale = Vector3.one;
            
            countdown.text = GameManager.instance.countdown.ToString();

            LeanTween.scale(countdown.rectTransform, countdownScale, 0.33f)
                .setEaseOutElastic();
            
            AudioManager.Play(SFX.Tick);
        }   
        else
        {
            GameObject go = countdown.transform.parent.gameObject;
            
            go.GetComponent<CanvasGroup>().LeanAlpha(0, 0.1f)
                .setOnComplete(() => go.SetActive(false));

            AudioManager.Play(SFX.Click);
        }   
    }

    private void OnTimesUp()
    {
        if (GameManager.instance.hasEnded)
            return;

        AudioManager.Play(SFX.Failure);
        LeanTween.cancel(clockID);

        if (GameManager.instance.failed)
            timesUp.GetComponentInChildren<Text>().text = "FAILED!";

        FadeIn(timesUp);
    }

    private void OnComplete()
    {
        if (GameManager.instance.hasEnded)
            return;

        AudioManager.Play(SFX.Success);
        LeanTween.cancel(clockID);

        if (!levelLoader.IsNextLevelAvailable())
            nextLevel.interactable = false;

        FadeIn(complete, () => ShowStar(0));
    }

    private void FadeIn(GameObject gameObject, Action onComplete = null)
    {
        CanvasGroup canvas = gameObject.GetComponent<CanvasGroup>();

        var seq = LeanTween.sequence();
        seq.append(1);
        seq.append(() => {
            canvas.alpha = 0;
            gameObject.SetActive(true);
        });
        seq.append(() => canvas.LeanAlpha(1, 0.1f));
        seq.append(onComplete);
    }

    private void ShowStar(int n)
    {
        if (n < GameManager.instance.rating)
        {
            var star = Instantiate(stars[n], stars[n].transform.parent);
            var scale = star.rectTransform.localScale;
            
            star.rectTransform.localScale = Vector3.zero;
            star.color = starColor;

            star.rectTransform.LeanScale(scale, 0.2f)
                .setEaseOutElastic()
                .setOnComplete(() => ShowStar(n + 1));
            
            AudioManager.Play(SFX.Collect);
        }
    }

    private void UpdateStarCount()
    {
        int count = GameManager.instance.rating;

        if (count < 0)
            starCount.text = "!";
        else
            starCount.text = count.ToString();
    }

    public void MainMenu()
    {
        AudioManager.Play(SFX.Click);
        Fade.instance.FadeOut(() => levelLoader.LoadMenu(false));
    }

    public void Restart()
    {
        AudioManager.Play(SFX.Click);
        Fade.instance.FadeOut(() => levelLoader.ReloadLevel());
    }

    public void SelectLevel()
    {
        AudioManager.Play(SFX.Click);
        Fade.instance.FadeOut(() => levelLoader.LoadMenu(true));
    }

    public void NextLevel()
    {
        AudioManager.Play(SFX.Click);
        Fade.instance.FadeOut(() => levelLoader.LoadNextLevel());
    }
}
