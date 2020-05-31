using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public GameObject levelPrefab;
        public int timeLimit;

        public bool unlocked;
        public int rating;
    }

#if UNITY_EDITOR
    [SerializeField]
    private bool unlockAll = false;
#endif

    public List<Level> levels;

    private static char[] savedData = null;

    // Loads saved data (player progression)
    private void Awake()
    {
        if (savedData == null)
        {
            if (PlayerPrefs.HasKey("savedData"))
                savedData = PlayerPrefs.GetString("savedData").ToCharArray(0, 12);
            else
                SetDefault();
        }    

        LoadSavedData();

#if UNITY_EDITOR
        if (unlockAll)
        {
            foreach (var level in levels)
            {
                level.unlocked = true;
            }
        }
#endif
    }

    // If there is no saved data then loads defaults
    private void SetDefault()
    {
        savedData = new char[12];

        savedData[0] = '0';

        for (int i = 1; i < levels.Count; i++)
        {
            savedData[i] = '9';
        }
    }

    // Setting progression based on saved data
    private void LoadSavedData()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (savedData[i] == '9')
            {
                levels[i].unlocked = false;
            }
            else
            {
                levels[i].unlocked = true;
                levels[i].rating = savedData[i] - '0';
            }
        }
    }

    public void SetRating(int index, int rating)
    {
        savedData[index] = (char)(rating + '0');
    }

    public void Unlock(int index)
    {
        savedData[index] = '0';
        levels[index].unlocked = true;
    }

    public void Save()
    {
#if !UNITY_EDITOR
        PlayerPrefs.SetString("savedData", new string(savedData));
#endif
    }

    public void ResetProgress()
    {
        SetDefault();
        Save();
        LoadSavedData();
    }
}
