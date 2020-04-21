using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LevelBounds : MonoBehaviour
{
    private LineRenderer boundsRenderer;

    public static readonly float upperBound =  4.5f,
                                 lowerBound = -5.5f,
                                 leftBound = -3.0f,
                                 rightBound = 3.0f;

    private Vector3[] boundsPositions;

    private void Start()
    {
        boundsRenderer = GetComponent<LineRenderer>();

        SetBoundsPositions();
    }

    private void SetBoundsPositions()
    {
        boundsPositions = new Vector3[4];

        boundsPositions[0] = new Vector3(leftBound, lowerBound);
        boundsPositions[1] = new Vector3(leftBound, upperBound);
        boundsPositions[2] = new Vector3(rightBound, upperBound);
        boundsPositions[3] = new Vector3(rightBound, lowerBound);

        boundsRenderer.SetPositions(boundsPositions);
    }

    public static bool CheckInBounds(Vector3 position)
    {
        if ((position.y >= lowerBound && position.y <= upperBound) &&
            (position.x >= leftBound && position.x <= rightBound))
            return true;
        else
            return false;
    }
}
