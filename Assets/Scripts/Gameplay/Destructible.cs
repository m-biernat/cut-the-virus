using UnityEngine;

// This class is parent for all destructibles in game
public abstract class Destructible : MonoBehaviour
{
    [HideInInspector]
    public Level level;
    
    protected System.Action onDestroy;

    public virtual void Destroy()
    {
        AudioManager.Play(SFX.Death);

        gameObject.GetComponent<Collider>().enabled = false;

        LeanTween.scale(gameObject, Vector3.zero, 0.4f)
            .setEaseInElastic()
            .setOnComplete(onDestroy);
    }
}
