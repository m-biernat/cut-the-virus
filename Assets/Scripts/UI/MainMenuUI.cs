using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public Transform content;
    public GameObject buttonPrefab;

    [Space]
    public LevelData levelData;
    public LevelLoader levelLoader;

    [Space]
    public GameObject mainMenu;
    public GameObject levelGrid;

    private GameObject currentView;

    [Space]
    public float fadeSpeed = 0.25f;

    void Start()
    {
        InitLevelGrid();

        if (LevelLoader.loadLevelGrid)
        {
            mainMenu.SetActive(false);
            levelGrid.SetActive(true);
            currentView = levelGrid;
        }
        else
        {
            mainMenu.SetActive(true);
            currentView = mainMenu;
        }
    }

    private void InitLevelGrid()
    {
        int levelCount = levelData.levels.Count;

        for (int i = 0; i < levelCount; i++)
        {
            GameObject go = Instantiate(buttonPrefab, content);

            GridButtonUI btn = go.GetComponent<GridButtonUI>();
            btn.SetUpButton(levelLoader, i, levelData.levels[i].unlocked);
        }
    }

    public void ChangeView(GameObject to)
    {
        AudioManager.Play(SFX.Click);

        FadeToView(currentView, to);
        currentView = to;
    }

    private void FadeToView(GameObject from, GameObject to)
    {
        CanvasGroup fromCanvas = from.GetComponent<CanvasGroup>();
        CanvasGroup toCanvas = to.GetComponent<CanvasGroup>();

        toCanvas.alpha = 0.0f;

        var seq = LeanTween.sequence();
        seq.append(fromCanvas.LeanAlpha(0, fadeSpeed));
        seq.append(() => {
            from.SetActive(false);
            to.SetActive(true);
        });
        seq.append(toCanvas.LeanAlpha(1, fadeSpeed));
    }

    public void QuitGame()
    {
        AudioManager.Play(SFX.Click);
        
        Application.Quit();
    }
}
