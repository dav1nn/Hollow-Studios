using System.Collections;
using UnityEngine;

public class BackgroundSoundFadeIn : MonoBehaviour
{
    public AudioSource backgroundSound; 
    public float fadeInDuration = 5f; 

    void Start()
    {
        if (backgroundSound != null)
        {
            backgroundSound.volume = 0f; 
            backgroundSound.Play(); 
            StartCoroutine(FadeInSound(backgroundSound, fadeInDuration));
        }
    }

    IEnumerator FadeInSound(AudioSource audioSource, float duration)
    {
        float elapsedTime = 0f;
        float targetVolume = 1f; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, targetVolume, elapsedTime / duration);
            yield return null;
        }

        audioSource.volume = targetVolume; 
    }
}
