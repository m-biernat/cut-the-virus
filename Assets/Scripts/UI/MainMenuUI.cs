using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public Transform content;
    public GameObject buttonPrefab;

    public LevelData levelData;
    public LevelLoader levelLoader;

    void Start()
    {
        int levelCount = levelData.levels.Count;
               
        for (int i = 0; i < levelCount; i++)
        {
            GameObject go = Instantiate(buttonPrefab, content);

            GridButtonUI btn = go.GetComponent<GridButtonUI>();
            btn.SetUpButton(levelLoader, i, levelData.levels[i].unlocked);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
