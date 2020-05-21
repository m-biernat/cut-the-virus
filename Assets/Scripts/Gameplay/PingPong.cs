using UnityEngine;

public class PingPong : MonoBehaviour
{
    public Transform destination;
    public float time;

    private int id = 0;

    private void Start()
    {
        id = LeanTween
            .moveLocal(gameObject, destination.position, time)
            .setLoopPingPong()
            .setEaseInOutSine()
            .id;
    }

    private void OnDestroy()
    {
        LeanTween.cancel(id);
    }
}
