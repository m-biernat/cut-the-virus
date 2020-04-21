using UnityEngine;

public class Virus : MonoBehaviour, IDestructible
{
    [HideInInspector]
    public Level level;

    public void Destroy()
    {
        level.Destroy(this);
    }
}
