using System.Collections;
using UnityEngine;

public class ExpandOnEnable : MonoBehaviour
{
    [SerializeField] private float expandDuration = 0.5f;
    private Vector3 originalScale;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(Expand());
    }

    IEnumerator Expand()
    {
        float elapsed = 0f;
        while (elapsed < expandDuration)
        {
            float t = elapsed / expandDuration;

            t = Mathf.SmoothStep(0f, 1f, t);

            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }
}
