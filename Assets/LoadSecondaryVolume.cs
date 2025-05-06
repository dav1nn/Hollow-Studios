using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSecondaryVolume : MonoBehaviour
{
    private void Awake()
    {
        AudioSource audio = GetComponent<AudioSource>();

        if (audio != null)
        {
            float volume = PlayerPrefs.GetFloat("SecondaryVolume", 1f);
            audio.volume = volume;
        }
    }
}

