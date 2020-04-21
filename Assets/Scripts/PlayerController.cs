using System.IO;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Plane plane;
    private Camera mainCamera;
    private Vector3 position;

    private RaycastHit[] hits;

    public GameObject playerPathRendererPrefab;
    private PlayerPathRenderer pathRenderer;

    private void Start()
    {
        plane = new Plane(Vector3.back, 0.0f);
        mainCamera = Camera.main;
        position = Vector3.one;

        GameObject go = Instantiate(playerPathRendererPrefab, transform.parent);
        pathRenderer = go.GetComponent<PlayerPathRenderer>();

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

                foreach (var hit in hits)
                {
                    Debug.Log(hit.collider.name);
                }

                pathRenderer.Clear();
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
        {
            position = nextPosition;
            DetectHits();
            
            pathRenderer.Draw(transform.position, position);
        }
    }

    private void DetectHits()
    {
        Vector3 heading = position - transform.position;

        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        hits = Physics.RaycastAll(transform.position, direction, distance);
    }

    private void OnGameEnd()
    {
        enabled = false;
    }
}
