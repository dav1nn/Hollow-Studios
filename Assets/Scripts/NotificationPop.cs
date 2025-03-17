using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotificationPop : MonoBehaviour
{
    [SerializeField] private GameObject notificationObject;
    [SerializeField] private float delayBeforeShow = 10f;
    [SerializeField] private float visibleDuration = 5f;
    [SerializeField] private float popDuration = 0.2f;
    [SerializeField] private GameObject panelToOpen;
    private Vector3 originalScale;
    private Coroutine notificationRoutine;

    private void Awake()
    {
        if (notificationObject)
        {
            originalScale = notificationObject.transform.localScale;
            notificationObject.SetActive(false);
        }
    }

    private void Start()
    {
        notificationRoutine = StartCoroutine(NotificationRoutine());
    }

    private IEnumerator NotificationRoutine()
    {
        yield return new WaitForSeconds(delayBeforeShow);
        if (!notificationObject) yield break;
        notificationObject.SetActive(true);
        notificationObject.transform.localScale = Vector3.zero;
        yield return StartCoroutine(AnimatePop(notificationObject, Vector3.zero, originalScale, popDuration));
        float timer = 0f;
        while (timer < visibleDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        yield return StartCoroutine(AnimatePop(notificationObject, originalScale, Vector3.zero, popDuration));
        notificationObject.SetActive(false);
    }

    public void OnDismissButtonClick()
    {
        if (notificationObject != null && notificationObject.activeSelf)
        {
            if (notificationRoutine != null)
            {
                StopCoroutine(notificationRoutine);
            }
            StartCoroutine(DismissAndOpenPanel());
        }
    }

    private IEnumerator DismissAndOpenPanel()
    {
        yield return StartCoroutine(AnimatePop(notificationObject, originalScale, Vector3.zero, popDuration));
        notificationObject.SetActive(false);
        if (panelToOpen != null)
        {
            panelToOpen.SetActive(true);
        }
    }

    private IEnumerator AnimatePop(GameObject obj, Vector3 fromScale, Vector3 toScale, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            obj.transform.localScale = Vector3.Lerp(fromScale, toScale, t);
            yield return null;
        }
        obj.transform.localScale = toScale;
    }
}






