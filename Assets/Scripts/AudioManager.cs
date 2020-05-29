using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static bool isMuted = false;

    private static AudioManager instance;

    public List<AudioSource> sfx;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Switches sound volume between 0 and 1
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

    // Plays certain sound effect
    public static void Play(SFX effectName)
    {
        instance.sfx[(int)effectName].Play();
    }
}

public enum SFX
{ 
    Collect,
    Death,
    Failure,
    Slash,
    Success,
    Click,
    Tick,
    Countdown,
    Go
};

