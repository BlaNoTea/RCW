using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public bool canPlaySounds = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 播放音效
    public void PlaySound(AudioClip clip)
    {
        if (canPlaySounds && clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Vector3.zero, 1.0f);
        }
    }
}
