using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneMusicVolume : MonoBehaviour
{
    private void Awake()
    {
        AudioSource audio = GetComponent<AudioSource>();

        if (audio != null)
        {
            float volume = PlayerPrefs.GetFloat("SceneMusicVolume", 1f);
            audio.volume = volume;
        }
    }
}

