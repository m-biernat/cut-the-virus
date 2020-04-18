using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [System.Serializable]
    public struct Level
    {
        public GameObject levelPrefab;
        public float timeLimit;

        public bool unlocked;
        public int rating;
    }

    public List<Level> levels;
}
