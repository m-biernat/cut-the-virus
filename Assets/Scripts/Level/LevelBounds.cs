using UnityEngine;

[ExecuteInEditMode]
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

    // Sets bounds positions for line renderer
    private void SetBoundsPositions()
    {
        boundsPositions = new Vector3[4];

        boundsPositions[0] = new Vector3(leftBound, lowerBound);
        boundsPositions[1] = new Vector3(leftBound, upperBound);
        boundsPositions[2] = new Vector3(rightBound, upperBound);
        boundsPositions[3] = new Vector3(rightBound, lowerBound);

        boundsRenderer.SetPositions(boundsPositions);
    }

    // Checks if point is in bounds and clamps it
    public static void KeepInBounds(ref Vector3 position)
    {
        if (position.y < lowerBound)
            position.y = lowerBound;
        else if (position.y > upperBound)
            position.y = upperBound;

        if (position.x < leftBound)
            position.x = leftBound;
        else if (position.x > rightBound)
            position.x = rightBound;
    }
}
