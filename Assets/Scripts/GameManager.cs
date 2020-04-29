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
    public event OnTickDelegate OnTick;

    public delegate void OnTimesUpDelegate();
    public event OnTimesUpDelegate OnTimesUp;

    public delegate void OnCompleteDelegate();
    public event OnCompleteDelegate OnComplete;

    public static GameManager instance;

    private void Start()
    {
        instance = this;
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
        //OnTimesUp += () => { };
        //OnComplete += () => { };

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
        yield return new WaitForSecondsRealtime(0.5f);

        OnTick();

        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(1.0f);

        for (int i = 0; i < timeLimit; i++)
        {
            yield return delay;

            OnTick();
        }

        OnTimesUp();
        Debug.Log("Time's up!");
        gameObject.SetActive(false);
    }
}
