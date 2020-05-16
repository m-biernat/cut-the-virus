using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private Image image;

    public Sprite soundOn, soundOff;

    private void Start()
    {
        image = GetComponent<Image>();

        if (AudioManager.isMuted)
            image.sprite = soundOff;
        else
            image.sprite = soundOn;
    }

    public void Toggle()
    {
        AudioManager.Toggle();

        if (image.sprite == soundOn)
            image.sprite = soundOff;
        else
            image.sprite = soundOn;
    }
}
