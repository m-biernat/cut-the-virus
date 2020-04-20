using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int timeLimit = 0,
                      timeLeft = 0;

    private Level level = null;

    private int alliesCount = 0, alliesDestroyed = 0,
                enemiesCount = 0, enemiesDestroyed = 0;

    public delegate void OnTickDelegate();
    public static OnTickDelegate OnTick;

    public delegate void OnTimesUpDelegate();
    public static OnTimesUpDelegate OnTimesUp;

    private void Start()
    {
        InitGame();
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

        // Init UI

        StartCoroutine(Timer());
    }

    private void OnAllyDestroy()
    {
        alliesDestroyed++;
        Debug.Log("Destroyed allies " + alliesDestroyed + " of " + alliesCount);
    }

    private void OnEnemyDestroy()
    {
        enemiesDestroyed++;
        Debug.Log("Destroyed enemies " + enemiesDestroyed + " of " + enemiesCount);

        if (enemiesDestroyed == enemiesCount)
        {
            Debug.Log("Level complete");
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
            
            Debug.Log("Time left: " + timeLeft + " of " + timeLimit + " s.");
        }

        OnTimesUp();

        Debug.Log("Time's up!");
    }
}
