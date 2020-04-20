using UnityEngine;

public class Virus : MonoBehaviour
{
    [HideInInspector]
    public Level level;

    public void Destroy()
    {
        level.Destroy(this);
    }
}
