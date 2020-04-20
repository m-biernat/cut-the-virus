using UnityEngine;

public class BloodCell : MonoBehaviour
{
    [HideInInspector]
    public Level level;

    public void Destroy()
    {
        level.Destroy(this);
    }
}
