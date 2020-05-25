using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Plane plane;
    private Camera mainCamera;
    private Vector3 position;

    private RaycastHit[] hits;

    public GameObject playerPathRendererPrefab;
    private PlayerPathRenderer pathRenderer;

    private bool isMovePossible, isPlayerMoving;

    private float upperBound;

    private void Start()
    {
        plane = new Plane(Vector3.back, 0.0f);
        mainCamera = Camera.main;
        position = Vector3.one;

        isMovePossible = false;
        isPlayerMoving = false;

        GameObject go = Instantiate(playerPathRendererPrefab, transform.parent);
        pathRenderer = go.GetComponent<PlayerPathRenderer>();

        upperBound = LevelBounds.upperBound + 0.1f;

        GameManager.instance.OnStart += OnGameStart;
        GameManager.instance.OnComplete += OnGameEnd;
        GameManager.instance.OnTimesUp += OnGameEnd;

        enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isPlayerMoving)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distanceToPlane;

            Vector3 nextPosition = Vector3.one;

            if (plane.Raycast(ray, out distanceToPlane))
                nextPosition = ray.GetPoint(distanceToPlane);

            if (nextPosition.y <= upperBound)
            {
                LevelBounds.KeepInBounds(ref nextPosition);
                position = nextPosition;

                DetectHits();
                CheckIfMoveIsPossible();

                pathRenderer.Draw(transform.position, position, isMovePossible);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (position != Vector3.one && isMovePossible && !isPlayerMoving)
            {
                isPlayerMoving = true;

                LeanTween.move(gameObject, position, .25f)
                    .setEaseInExpo()
                    .setOnComplete(() =>
                    {
                        position = Vector3.one;

                        foreach (var hit in hits)
                        {
                            hit.collider.gameObject.GetComponent<Destructible>().Destroy();
                        }

                        isPlayerMoving = false;
                    });

                AudioManager.Play(SFX.Slash);
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
        bool virusFound = false, fatFound = false;
        Vector3 lastVirusPosition = Vector3.zero;

        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Virus")
            {
                virusFound = true;
                lastVirusPosition = hit.collider.transform.position;
            }

            if (hit.collider.tag == "Fat")
                fatFound = true;
        }

        isMovePossible = virusFound && !fatFound;

        if (isMovePossible)
        {
            float distance = Vector3.Distance(lastVirusPosition, position);

            if (distance < 0.5f)
                isMovePossible = false;
        }
    }

    private void OnGameStart()
    {
        enabled = true;
    }

    private void OnGameEnd()
    {
        pathRenderer.Clear();
        enabled = false;
    }
}
