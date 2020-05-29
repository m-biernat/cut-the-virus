using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This class handles platform specific stuff (scaling, android action handlin etc.)
public class PlatformManager : MonoBehaviour
{
    public CanvasScaler canvasScaler;
    public LevelLoader levelLoader;

#if UNITY_WEBGL
    private void Awake()
    {
        canvasScaler.matchWidthOrHeight = 1.0f;
    }
#endif

#if UNITY_ANDROID
    private void Awake()
    {
        FixAspectRatio();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackButtonEvent();
    }

    private void FixAspectRatio()
    {
        float screenWidth = Screen.width, screenHeight = Screen.height;

        int aspectRatio = (int)(screenWidth / screenHeight * 100);

        switch (aspectRatio)
        {
            case 46:
                SetFOV(70.0f);
                break;
            case 48:
                SetFOV(67.8f);
                break;
            case 50:
                SetFOV(66.0f);
                break;
            case 56:
                SetFOV(60.5f);
                break;
        }
    }

    private void SetFOV(float value)
    {
        Camera.main.fieldOfView = value;
    }

    private void BackButtonEvent()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            AudioManager.Play(SFX.Click);
            Application.Quit();
        }
        else
        {
            AudioManager.Play(SFX.Click);
            Fade.instance.FadeOut(() => levelLoader.LoadMenu(false));
        }
    }
#endif
}
