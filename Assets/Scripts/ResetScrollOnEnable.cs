using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResetScrollOnEnableMulti : MonoBehaviour
{
    [SerializeField] private ScrollRect[] scrollRects;

    private void OnEnable()
    {
        StartCoroutine(ResetScroll());
    }

    private IEnumerator ResetScroll()
    {
        yield return null;
        Canvas.ForceUpdateCanvases();
        foreach (var sr in scrollRects)
        {
            sr.verticalNormalizedPosition = 1f;
        }
    }
}


