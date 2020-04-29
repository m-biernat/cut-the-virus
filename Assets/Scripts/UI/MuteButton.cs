using UnityEngine;

public class MuteButton : MonoBehaviour
{
    private void Start()
    {
        // Check if muted and set icon
    }

    public void Toggle()
    {
        AudioManager.Toggle();
        // Set icon
    }
}
