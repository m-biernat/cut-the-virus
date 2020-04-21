using UnityEngine;

public class PlayerPathRenderer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer = null;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    public void Draw(Vector3 startPosition, Vector3 endPosition)
    {
        transform.position = endPosition;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);

        lineRenderer.enabled = true;
        spriteRenderer.enabled = true;
    }

    public void Clear()
    {
        lineRenderer.enabled = false;
        spriteRenderer.enabled = false;
    }
}
