using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DoubleClickFlicker : MonoBehaviour, IPointerClickHandler
{
    public GameObject glitchOverlay;
    public float doubleClickThreshold = 0.3f;
    public float flickerDuration = 0.4f;
    public float flickerInterval = 0.05f;

    private float lastClickTime;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickThreshold)
            StartCoroutine(FlickerRoutine());
        lastClickTime = Time.time;
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

