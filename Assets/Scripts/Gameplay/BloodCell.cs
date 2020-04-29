using UnityEngine;

public class BloodCell : MonoBehaviour, IDestructible
{
    [HideInInspector]
    public Level level;

    public void Destroy()
    {
        level.Destroy(this);
    }
}
