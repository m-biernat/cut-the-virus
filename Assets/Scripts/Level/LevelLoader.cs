using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public LevelData levelData;

    public static int currentLevelIndex;
    public static LevelData.Level levelToLoad;

    public static bool loadLevelGrid;

    public void LoadLevel(int index)
    {
        currentLevelIndex = index;
        levelToLoad = levelData.levels[index];
        
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadNextLevel()
    {
        if (IsNextLevelAvailable())
        {
            LoadLevel(currentLevelIndex + 1);
        }
    }

    public bool IsNextLevelAvailable()
    {
        int nextLevelIndex = currentLevelIndex + 1;

        if (levelData.levels[nextLevelIndex].unlocked)
            return true;
        else
            return false;
    }

    public void LoadMenu(bool levelGrid)
    {
        if (levelGrid)
            loadLevelGrid = true;
        else
            loadLevelGrid = false;

        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
