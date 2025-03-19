using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FolderActivate : MonoBehaviour
{
    public GameObject objectToFadeIn;
    public GameObject permanentDeactivator;
    public List<GameObject> objectsToDeactivate;
    public float fadeDuration = 1f;

    private Coroutine handleTimedEventsCoroutine;

    private void Start()
    {
        handleTimedEventsCoroutine = StartCoroutine(HandleTimedEvents());
    }

    private void Update()
    {
        if (permanentDeactivator != null && permanentDeactivator.activeSelf)
        {
            foreach (GameObject go in objectsToDeactivate)
            {
                if (go != null)
                {
                    go.SetActive(false);
                }
            }
        }
    }

    public void SkipAndDeactivateNow()
    {
        if (handleTimedEventsCoroutine != null)
        {
            StopCoroutine(handleTimedEventsCoroutine);
        }

        foreach (GameObject go in objectsToDeactivate)
        {
            if (go != null)
            {
                go.SetActive(false);
            }
        }

        StartCoroutine(FadeInNow());
    }

    private IEnumerator FadeInNow()
    {
        if (objectToFadeIn != null)
        {
            objectToFadeIn.SetActive(true);
            CanvasGroup cg = objectToFadeIn.GetComponent<CanvasGroup>();
            if (cg == null) cg = objectToFadeIn.AddComponent<CanvasGroup>();

            cg.alpha = 0f;
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                cg.alpha = Mathf.Clamp01(elapsed / fadeDuration);
                yield return null;
            }

            cg.alpha = 1f;
        }
    }

    private IEnumerator HandleTimedEvents()
    {
        yield return new WaitForSeconds(1f);

        List<GameObject> activeObjects = new List<GameObject>();
        foreach (GameObject go in objectsToDeactivate)
        {
            if (go != null && go.activeSelf)
            {
                activeObjects.Add(go);
            }
        }

        for (int i = 0; i < activeObjects.Count; i++)
        {
            int randomIndex = Random.Range(i, activeObjects.Count);
            var temp = activeObjects[i];
            activeObjects[i] = activeObjects[randomIndex];
            activeObjects[randomIndex] = temp;
        }

        foreach (GameObject go in activeObjects)
        {
            go.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }

        float timeSoFar = 80f + activeObjects.Count * 0.1f;
        float remaining = 12f - timeSoFar;
        if (remaining > 0f) yield return new WaitForSeconds(remaining);

        if (objectToFadeIn == null) yield break;

        objectToFadeIn.SetActive(true);
        CanvasGroup cg2 = objectToFadeIn.GetComponent<CanvasGroup>();
        if (cg2 == null) cg2 = objectToFadeIn.AddComponent<CanvasGroup>();

        cg2.alpha = 0f;
        float elapsed2 = 0f;

        while (elapsed2 < fadeDuration)
        {
            elapsed2 += Time.deltaTime;
            cg2.alpha = Mathf.Clamp01(elapsed2 / fadeDuration);
            yield return null;
        }

        cg2.alpha = 1f;
    }
}