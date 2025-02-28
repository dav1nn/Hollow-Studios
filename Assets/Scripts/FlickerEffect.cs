using UnityEngine;
using System.Collections;

public class FlickerEffect : MonoBehaviour
{
    public GameObject glitchOverlay;
    public float flickerDuration = 0.4f;
    public float flickerInterval = 0.05f;

    public void StartFlicker()
    {
        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        float endTime = Time.time + flickerDuration;
        while (Time.time < endTime)
        {
            glitchOverlay.SetActive(!glitchOverlay.activeSelf);
            yield return new WaitForSeconds(flickerInterval);
        }
        glitchOverlay.SetActive(false);
    }
}

