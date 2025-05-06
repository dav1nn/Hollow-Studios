using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        float savedVolume = PlayerPrefs.GetFloat("SceneMusicVolume", 1f);
        audioSource.volume = savedVolume;
    }
}
