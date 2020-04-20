using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Plane plane;
    private Camera mainCamera;
    private Vector3 position;

    private void Start()
    {
        plane = new Plane(Vector3.back, 0.0f);
        mainCamera = Camera.main;
        position = Vector3.one;

        //GameManager.OnComplete += OnGameEnd;
        //GameManager.OnTimesUp += OnGameEnd;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CalculatePosition();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (position != Vector3.one)
            {
                transform.position = position;
                position = Vector3.one;
            }
        }
    }

    private void CalculatePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        float distanceToPlane;

        Vector3 nextPosition = Vector3.one;

        if (plane.Raycast(ray, out distanceToPlane))
            nextPosition = ray.GetPoint(distanceToPlane);

        if (LevelBounds.CheckInBounds(nextPosition))
            position = nextPosition;
    }

    private void OnGameEnd()
    {
        enabled = false;
    }
}
