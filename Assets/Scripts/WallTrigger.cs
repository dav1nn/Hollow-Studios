using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WallTrigger : MonoBehaviour
{
    public GameObject[] walls;
    public CodeFlicker[] codeFlickers;
    public string newCharacters = "HAHAHAH";
    public GameObject sphere;
    public Volume postProcessVolume;
    public Image crosshair;
    public float fadeDuration = 2f;
    public string nextSceneName = "Pro 3";

    private ColorAdjustments colorAdjustments;
    private bool triggered = false;

    public AudioSource soundEffect; 
    public AudioSource backgroundMusic; 
    public float musicFadeOutDuration = 4f; 

    void Start()
    {
        if (postProcessVolume != null)
            postProcessVolume.profile.TryGet(out colorAdjustments);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            
            if (soundEffect != null)
            {
                soundEffect.Play();
            }

            
            if (backgroundMusic != null && backgroundMusic.isPlaying)
            {
                StartCoroutine(FadeOutMusic(backgroundMusic, musicFadeOutDuration));
            }

            foreach (GameObject wall in walls)
            {
                if (wall != null)
                    wall.SetActive(true);
            }

            StartCoroutine(ChangeTextsAfterDelay());
            if (sphere != null)
                StartCoroutine(RemoveSphereAfterDelay());
            StartCoroutine(FadeToBlack());
        }
    }

    IEnumerator ChangeTextsAfterDelay()
    {
        yield return new WaitForSeconds(0f);
        foreach (CodeFlicker codeFlicker in codeFlickers)
        {
            if (codeFlicker != null)
                codeFlicker.UpdateToRed(newCharacters);
        }
    }

    IEnumerator RemoveSphereAfterDelay()
    {
        yield return new WaitForSeconds(0f);
        sphere.SetActive(false);
    }

    IEnumerator FadeToBlack()
    {
        yield return new WaitForSeconds(5f);
        if (postProcessVolume == null || colorAdjustments == null)
            yield break;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            colorAdjustments.colorFilter.value = Color.Lerp(Color.white, Color.black, timer / fadeDuration);
            if (crosshair != null)
            {
                Color crosshairColor = crosshair.color;
                crosshairColor.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
                crosshair.color = crosshairColor;
            }
            yield return null;
        }
        colorAdjustments.colorFilter.value = Color.black;
        if (crosshair != null)
            crosshair.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator FadeOutMusic(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / duration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}










