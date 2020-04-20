using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<BloodCell> bloodCells;
    public List<Virus> viruses;

    public delegate void OnAllyDestroyDelegate();
    public OnAllyDestroyDelegate OnAllyDestroy;

    public delegate void OnEnemyDestroyDelegate();
    public OnEnemyDestroyDelegate OnEnemyDestroy;

    private void Start()
    {
        foreach (var bloodCell in bloodCells)
        {
            bloodCell.level = this;
        }

        foreach (var virus in viruses)
        {
            virus.level = this;
        }
    }

    public void Destroy(BloodCell bloodCell)
    {
        bloodCells.Remove(bloodCell);
        Destroy(bloodCell.gameObject);

        OnAllyDestroy();
    }

    public void Destroy(Virus virus)
    {
        viruses.Remove(virus);
        Destroy(virus.gameObject);
        
        OnEnemyDestroy();
    }
}

