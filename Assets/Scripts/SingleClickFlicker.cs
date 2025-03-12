using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SingleClickFlicker : MonoBehaviour, IPointerClickHandler
{
    public GameObject glitchOverlay;
    public float flickerDuration = 0.4f;
    public float flickerInterval = 0.05f;

    private bool hasActivated;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasActivated)
        {
            hasActivated = true;
            StartCoroutine(FlickerRoutine());
        }
    }

    private IEnumerator FlickerRoutine()
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



