using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RedFilterTimer : MonoBehaviour
{
    public Volume postProcessVolume;
    public Image crosshair;
    public float maxTime = 120f;
    public string restartSceneName = ""; 

    private Vignette vignette;
    private float elapsedTime = 0f;
    private bool safeZoneReached = false;
    private bool timerRunning = true;

    void Start()
    {
        if (postProcessVolume != null && postProcessVolume.profile.TryGet(out vignette))
        {
            vignette.intensity.Override(0f);
            vignette.color.Override(Color.red);
        }
        StartCoroutine(RunTimer());
    }

    IEnumerator RunTimer()
    {
        while (elapsedTime < maxTime && timerRunning)
        {
            elapsedTime += Time.deltaTime;
            if (vignette != null)
            {
                float intensity = Mathf.Clamp01(elapsedTime / maxTime) * 0.8f;
                vignette.intensity.Override(intensity);
            }
            yield return null;
        }

        if (!safeZoneReached)
        {
            if (!string.IsNullOrEmpty(restartSceneName))
                SceneManager.LoadScene(restartSceneName);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void DisableTimerAndEffect()
    {
        if (!safeZoneReached)
        {
            safeZoneReached = true;
            timerRunning = false;
            StartCoroutine(FadeOutRedEffect());
        }
    }

    IEnumerator FadeOutRedEffect()
    {
        float timer = 0f;
        float startIntensity = vignette != null ? vignette.intensity.value : 0f;
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            float fadeIntensity = Mathf.Lerp(startIntensity, 0f, timer / 2f);
            if (vignette != null)
                vignette.intensity.Override(fadeIntensity);
            yield return null;
        }
        if (vignette != null)
            vignette.intensity.Override(0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisableTimerAndEffect();
        }
    }
}

