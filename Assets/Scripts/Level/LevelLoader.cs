using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public LevelData levelData;

    public static int currentLevelIndex;
    public static LevelData.Level levelToLoad;

    public void LoadLevel(int index)
    {
        currentLevelIndex = index;
        levelToLoad = levelData.levels[index];
        
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
