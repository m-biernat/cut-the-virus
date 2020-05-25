using UnityEngine;

public class BrokenStar : MonoBehaviour
{
    public Sprite sprite;

    private void Start()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = Vector3.zero;

        var seq = LeanTween.sequence();
        seq.append(() => LeanTween.scale(gameObject, scale, 0.1f).setEaseOutExpo());
        seq.append(0.6f);
        seq.append(() => LeanTween.scale(gameObject, Vector3.zero, 0.1f).setEaseInExpo());
    }
}
