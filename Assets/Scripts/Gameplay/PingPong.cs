using UnityEngine;

public class PingPong : MonoBehaviour
{
    public Transform destination;
    public float time;

    [Space]
    public float delay = 0;

    private Vector3 origin;
    private bool toOrigin = true;

    private void Start()
    {
        origin = transform.position;

        if (delay == 0)
            Loop();
        else
            DelayedLoop(destination.position);
    }

    // Animates moving from point A -> B and B -> A endlessly without delay
    private void Loop()
    {
        LeanTween
            .moveLocal(gameObject, destination.position, time)
            .setLoopPingPong()
            .setEaseInOutSine();
    }

    // Animates moving from point A -> B and B -> A endlessly with delay
    private void DelayedLoop(Vector3 moveTo)
    {
        LeanTween
            .moveLocal(gameObject, moveTo, time)
            .setEaseInOutSine()
            .setDelay(delay)
            .setOnComplete(() =>
            {
                if (toOrigin)
                {
                    toOrigin = false;
                    DelayedLoop(origin);
                }
                else
                {
                    toOrigin = true;
                    DelayedLoop(destination.position);
                }
            });
    }

    private void OnDestroy()
    {
        LeanTween.cancel(gameObject);
    }
}
