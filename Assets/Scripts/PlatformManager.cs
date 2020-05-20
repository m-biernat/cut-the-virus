using UnityEngine;
using UnityEngine.UI;

public class PlatformManager : MonoBehaviour
{
    public CanvasScaler canvasScaler;

    private void Awake()
    {
#if UNITY_WEBGL
        canvasScaler.matchWidthOrHeight = 1.0f;
#endif

#if UNITY_ANDROID
        FixAspectRatio();
#endif
    }

    private void FixAspectRatio()
    {
        float screenWidth = Screen.width, screenHeight = Screen.height;

        int aspectRatio = (int)(screenWidth / screenHeight * 100);

        switch (aspectRatio)
        {
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
}
