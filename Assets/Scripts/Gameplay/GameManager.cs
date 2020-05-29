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

    public event Action OnCountdown;
    public event Action OnStart;

    public event Action OnTick;
    public event Action OnAllyKilled;

    public event Action OnTimesUp;
    public event Action OnComplete;

    public static GameManager instance;

    [HideInInspector] 
    public int rating = 0;
    [SerializeField]
    private LevelData levelData = null;

    [HideInInspector]
    public bool failed = false;

    [HideInInspector]
    public bool hasEnded = false;

    public int countdown = 3;

    private void Start()
    {
        instance = this;
        InitGame();
        //Debug.Log($"Game has started with {enemiesCount} enemies and {alliesCount} allies.");
    }

    // Initializes the game (selected level) in Game scene
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

    // Executes when BloodCell is destoyed
    private void OnAllyDestroy()
    {
        alliesDestroyed++;
        //Debug.Log($"Destroyed allies {alliesDestroyed} of {alliesCount}");

        UpdateRating();
        OnAllyKilled();

        if (alliesDestroyed > 3)
            failed = true;     
    }

    // Executes when Virus is destoyed
    private void OnEnemyDestroy()
    {
        enemiesDestroyed++;
        //Debug.Log($"Destroyed enemies {enemiesDestroyed} of {enemiesCount}");

        if (enemiesDestroyed == enemiesCount || failed)
        {
            if (failed)
            {
                OnTimesUp();
                hasEnded = true;
                gameObject.SetActive(false);
            }
            else
            {
                Complete();
                OnComplete();
                hasEnded = true;
                gameObject.SetActive(false);
            }
        }
    }

    // Handles clock and timings in game
    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.33f);

        for (; countdown >= 0; countdown--)
        {
            OnCountdown();
            
            if (countdown > 0)
                yield return delay;
        }

        yield return new WaitForSecondsRealtime(0.1f);

        OnStart();

        delay = new WaitForSecondsRealtime(1.0f);

        for (; timeLeft > 0 ; timeLeft--)
        {
            OnTick();
            yield return delay;
        }

        OnTimesUp();
        hasEnded = true;
        gameObject.SetActive(false);
    }

    private void UpdateRating()
    {
        rating = 3 - alliesDestroyed;
    }

    // Executes when game is in "complete" state
    public void Complete()
    {
        int index = LevelLoader.currentLevelIndex;
        var level = levelData.levels[index];

        bool progress = false;

        //Debug.Log("Allies destroyed " + alliesDestroyed);
        UpdateRating();

        if (level.rating < rating)
        {
            levelData.SetRating(index, rating);
            progress = true;
        }

        index++;

        if (index < levelData.levels.Count 
            && !levelData.levels[index].unlocked)
        {
            levelData.Unlock(index);
            progress = true;
        }
            
        if (progress)
            levelData.Save();
    }

    private void OnDestroy()
    {
        LeanTween.cancelAll();
    }
}
