using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Plane plane;
    private Camera mainCamera;
    private Vector3 position;

    private RaycastHit[] hits;

    public GameObject playerPathRendererPrefab;
    private PlayerPathRenderer pathRenderer;

    private bool isMovePossible;

    private void Start()
    {
        plane = new Plane(Vector3.back, 0.0f);
        mainCamera = Camera.main;
        position = Vector3.one;

        isMovePossible = false;

        GameObject go = Instantiate(playerPathRendererPrefab, transform.parent);
        pathRenderer = go.GetComponent<PlayerPathRenderer>();

        GameManager.OnComplete += OnGameEnd;
        GameManager.OnTimesUp += OnGameEnd;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
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
                CheckIfMoveIsPossible();
                pathRenderer.Draw(transform.position, position, isMovePossible);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (position != Vector3.one && isMovePossible)
            {
                transform.position = position;
                position = Vector3.one;

                foreach (var hit in hits)
                {
                    hit.collider.gameObject.GetComponent<IDestructible>().Destroy();
                }
            }

            pathRenderer.Clear();
        }
    }

    private void DetectHits()
    {
        Vector3 heading = position - transform.position;

        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        hits = Physics.RaycastAll(transform.position, direction, distance);
    }

    private void CheckIfMoveIsPossible()
    { 
        foreach(var hit in hits)
        {
            if (hit.collider.tag == "Virus")
            {
                isMovePossible = true;
                return;
            }
        }
        isMovePossible = false;
    }

    private void OnGameEnd()
    {
        pathRenderer.Clear();
        enabled = false;
    }
}
