using UnityEngine;
using System.Collections;

public class GlitchEffect : MonoBehaviour
{
    public GameObject glitchOverlay;
    public float glitchDuration = 0.7f;

    void Start()
    {
        glitchOverlay.SetActive(false);
    }

    public void TriggerGlitch()
    {
        StartCoroutine(ShowGlitch());
    }

    IEnumerator ShowGlitch()
    {
        glitchOverlay.SetActive(true);
        yield return new WaitForSeconds(glitchDuration);
        glitchOverlay.SetActive(false);
    }
}

