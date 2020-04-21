using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image clockFill;
    public Text clockText;

    private int time, currTime;
    
    void Start()
    {
        GameManager.OnTick += OnClockUpdate;
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
        float fillAmount = currTime / time;
        clockFill.fillAmount = fillAmount;
    }
}
