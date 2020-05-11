using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int timeLeft = 0;

    private Level level = null;

    private int alliesCount = 0, alliesDestroyed = 0,
                enemiesCount = 0, enemiesDestroyed = 0;

    public GameUI gameUI;

    public event Action OnTick;
    public event Action OnTimesUp;
    public event Action OnComplete;

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
        timeLeft = LevelLoader.levelToLoad.timeLimit;

        level.OnAllyDestroy += OnAllyDestroy;
        level.OnEnemyDestroy += OnEnemyDestroy;

        alliesCount = level.bloodCells.Count;
        enemiesCount = level.viruses.Count;

        level.enabled = true;

        gameUI.Init(timeLeft);
        
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

        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(1.0f);

        for (; timeLeft > 0 ; timeLeft--)
        {
            OnTick();
            yield return delay;
        }

        OnTimesUp();
        Debug.Log("Time's up!");
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        LeanTween.cancelAll();
    }
}
