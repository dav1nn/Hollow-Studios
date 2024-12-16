using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyPressSound : MonoBehaviour
{
    public TMP_InputField inputField;  
    public AudioSource typingAudioSource;  
    public AudioSource deleteAudioSource;  

    private string previousText = ""; 

    void Update()
    {
        if (inputField.isFocused)
        {
            
            if (Input.anyKeyDown && !Input.GetKey(KeyCode.Backspace))
            {
                PlayTypingSound();
            }

            
            if (Input.GetKey(KeyCode.Backspace) && inputField.text.Length < previousText.Length)
            {
                PlayDeleteSound();
            }

            
            previousText = inputField.text;
        }
    }

    void PlayTypingSound()
    {
        if (typingAudioSource != null)
        {
            typingAudioSource.PlayOneShot(typingAudioSource.clip);
        }
    }

    void PlayDeleteSound()
    {
        if (deleteAudioSource != null)
        {
            deleteAudioSource.PlayOneShot(deleteAudioSource.clip);
        }
    }
}







