using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<BloodCell> bloodCells;
    public List<Virus> viruses;

    public event Action OnAllyDestroy;
    public event Action OnEnemyDestroy;

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

    public void DestroyAlly()
    {
        OnAllyDestroy();
    }

    public void Destroy(Virus virus)
    {
        viruses.Remove(virus);
        Destroy(virus.gameObject);
        
        OnEnemyDestroy();
    }
}

