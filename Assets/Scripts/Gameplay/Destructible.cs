﻿using UnityEngine;

public abstract class Destructible : MonoBehaviour
{
    [HideInInspector]
    public Level level;
    
    protected System.Action onDestroy;

    public virtual void Destroy()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        LeanTween.scale(gameObject, Vector3.zero, 0.4f)
            .setEaseInElastic()
            .setOnComplete(onDestroy);
    }
}
