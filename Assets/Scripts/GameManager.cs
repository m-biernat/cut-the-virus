using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int timeLimit = 0,
                      timeLeft = 0;

    private Level level = null;

    private int alliesCount = 0, alliesDestroyed = 0,
                enemiesCount = 0, enemiesDestroyed = 0;

    public GameUI gameUI;

    public delegate void OnTickDelegate();
    public static event OnTickDelegate OnTick;

    public delegate void OnTimesUpDelegate();
    public static event OnTimesUpDelegate OnTimesUp;

    public delegate void OnCompleteDelegate();
    public static event OnCompleteDelegate OnComplete;

    private void Start()
    {
        InitGame();
        Debug.Log($"Game has started with {enemiesCount} enemies and {alliesCount} allies.");
    }

    private void InitGame()
    {
        GameObject go = Instantiate(LevelLoader.levelToLoad.levelPrefab);

        level = go.GetComponent<Level>();
        timeLimit = LevelLoader.levelToLoad.timeLimit;

        level.OnAllyDestroy += OnAllyDestroy;
        level.OnEnemyDestroy += OnEnemyDestroy;

        alliesCount = level.bloodCells.Count;
        enemiesCount = level.viruses.Count;

        level.enabled = true;

        gameUI.Init(timeLimit);
        
        //OnTick += () => { };
        OnTimesUp += () => { };
        OnComplete += () => { };

        StartCoroutine(Timer());
    }

    private void OnAllyDestroy()
    {
        alliesDestroyed++;
        Debug.Log($"Destroyed allies {alliesDestroyed} of {alliesCount}");
    }

    private void OnEnemyDestroy()
    {
        enemiesDestroyed++;
        Debug.Log($"Destroyed enemies {enemiesDestroyed} of {enemiesCount}");

        if (enemiesDestroyed == enemiesCount)
        {
            OnComplete();
            Debug.Log("Level complete");
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Timer()
    {
        timeLeft = timeLimit;

        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(1.0f);

        for (int i = 0; i < timeLimit; i++)
        {
            yield return delay;

            timeLeft--;
            OnTick();
            //Debug.Log($"Time left: {timeLeft} of {timeLimit} s.");
        }

        OnTimesUp();
        Debug.Log("Time's up!");
        gameObject.SetActive(false);
    }
}
