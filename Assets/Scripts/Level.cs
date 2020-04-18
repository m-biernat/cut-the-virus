using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Level : MonoBehaviour
{
    private LineRenderer _boundsRenderer;

    private float _upperBound =  5.45f,
                  _lowerBound = -4.5f,
                  _leftBound = -3.5f,
                  _rightBound = 3.5f;

    private Vector3[] _boundsPositions;

    private void Start()
    {
        _boundsRenderer = GetComponent<LineRenderer>();

        SetBoundsPositions();
    }

    private void SetBoundsPositions()
    {
        _boundsPositions = new Vector3[4];

        _boundsPositions[0] = new Vector3(_leftBound, _lowerBound);
        _boundsPositions[1] = new Vector3(_leftBound, _upperBound);
        _boundsPositions[2] = new Vector3(_rightBound, _upperBound);
        _boundsPositions[3] = new Vector3(_rightBound, _lowerBound);

        _boundsRenderer.SetPositions(_boundsPositions);
    }

    public bool CheckInBounds(Vector3 position)
    {
        if ((position.x >= _lowerBound && position.x <= _upperBound) &&
            (position.y >= _leftBound && position.y <= _rightBound))
            return true;
        else
            return false;
    }
}
