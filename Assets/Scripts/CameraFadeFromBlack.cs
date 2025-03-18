using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CameraFadeFromBlack : MonoBehaviour
{
    public Volume postProcessVolume;
    public Image crosshair; 
    public float fadeDuration = 2f;

    private ColorAdjustments colorAdjustments;

    void Start()
    {
        if (postProcessVolume != null && postProcessVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.colorFilter.value = Color.black; 
            if (crosshair != null)
            {
                Color crosshairColor = crosshair.color;
                crosshairColor.a = 0f; 
                crosshair.color = crosshairColor;
            }
            StartCoroutine(FadeFromBlack());
        }
        else
        {
            Debug.LogError("Post-process volume or Color Adjustments not assigned!");
        }
    }

    IEnumerator FadeFromBlack()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            
            colorAdjustments.colorFilter.value = Color.Lerp(Color.black, Color.white, t);

            
            if (crosshair != null)
            {
                Color crosshairColor = crosshair.color;
                crosshairColor.a = Mathf.Lerp(0f, 1f, t);
                crosshair.color = crosshairColor;
            }

            yield return null;
        }

        
        colorAdjustments.colorFilter.value = Color.white;
        if (crosshair != null)
        {
            Color crosshairColor = crosshair.color;
            crosshairColor.a = 1f;
            crosshair.color = crosshairColor;
        }
    }
}
