using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static bool isMuted = false;

    private static AudioManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static void Toggle()
    {
        switch (isMuted)
        {
            case true:
                AudioListener.volume = 1;
                isMuted = false;
                break;

            case false:
                AudioListener.volume = 0;
                isMuted = true;
                break;
        }
    }
}
