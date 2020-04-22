using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image clockFill;
    public Text clockText;

    private int time, currTime;

    public GameObject timesUp, complete;

    public LevelLoader levelLoader;
    
    void Start()
    {
        GameManager.OnTick += OnClockUpdate;
        GameManager.OnTimesUp += OnTimesUp;
        GameManager.OnComplete += OnComplete;
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
        clockText.text = currTime.ToString();
        clockFill.fillAmount = currTime / (float)time;
    }

    private void OnTimesUp()
    {
        timesUp.SetActive(true);
    }

    private void OnComplete()
    {
        complete.SetActive(true);
    }

    public void MainMenu()
    {
        levelLoader.LoadMenu();
    }

    public void Restart()
    {
        levelLoader.LoadLevel(LevelLoader.currentLevelIndex);
    }

}
