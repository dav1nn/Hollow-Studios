using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResetScrollOnEnable : MonoBehaviour
{
    [SerializeField] private ScrollRect momScrollRect;
    [SerializeField] private ScrollRect dadScrollRect;
    [SerializeField] private ScrollRect harryScrollRect;
    [SerializeField] private ScrollRect anaScrollRect;

    private void OnEnable()
    {
        StartCoroutine(ResetAllScrolls());
    }

    private IEnumerator ResetAllScrolls()
    {
        yield return null;
        Canvas.ForceUpdateCanvases();

        if (momScrollRect)    momScrollRect.verticalNormalizedPosition = 1f;
        if (dadScrollRect)    dadScrollRect.verticalNormalizedPosition = 1f;
        if (harryScrollRect)  harryScrollRect.verticalNormalizedPosition = 1f;
        if (anaScrollRect)    anaScrollRect.verticalNormalizedPosition = 1f;
    }
}



