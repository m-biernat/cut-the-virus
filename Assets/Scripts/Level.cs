using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<GameObject> bloodCells;
    public List<GameObject> viruses;

    public delegate void OnAllyDestroyDelegate();
    public OnAllyDestroyDelegate OnAllyDestroy;

    public delegate void OnEnemyDestroyDelegate();
    public OnEnemyDestroyDelegate OnEnemyDestroy;

    private void Start()
    {
        foreach (var bloodCell in bloodCells)
        {
            // Add reference to Level
        }

        foreach (var virus in viruses)
        {
            // Add reference to Level
        }
    }

    public void DestroyAlly(GameObject go)
    {
        bloodCells.Remove(go);
        Destroy(go);

        OnAllyDestroy();
    }

    public void DestroyEnemy(GameObject go)
    {
        viruses.Remove(go);
        Destroy(go);
        
        OnEnemyDestroy();
    }
}

