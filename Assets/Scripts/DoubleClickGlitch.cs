using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DoubleClickGlitch : MonoBehaviour, IPointerClickHandler
{
    public GameObject glitchOverlay;
    public float glitchDuration = 2f;
    public float doubleClickThreshold = 0.3f;

    private float lastClickTime = 0f;

    void Start()
    {
        if (glitchOverlay != null)
            glitchOverlay.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            StartCoroutine(ShowGlitch());
        }
        lastClickTime = Time.time;
    }

    IEnumerator ShowGlitch()
    {
        if (glitchOverlay != null)
        {
            glitchOverlay.SetActive(true);
            yield return new WaitForSeconds(glitchDuration);
            glitchOverlay.SetActive(false);
        }
    }
}

