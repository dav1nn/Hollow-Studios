using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ImageEnlarger : MonoBehaviour
{
    public GameObject enlargePanel;
    public Image enlargePanelImage;
    public TMP_Text descriptionText;
    public Image[] images;
    public string[] descriptions;
    private Vector3 originalScale;

    private void Awake()
    {
        if (enlargePanel != null)
        {
            bool wasActive = enlargePanel.activeSelf;
            enlargePanel.SetActive(true);
            originalScale = enlargePanel.transform.localScale;
            enlargePanel.SetActive(wasActive);
        }
    }

    public void EnlargeImage(int index)
    {
        if (index < 0 || index >= images.Length || index >= descriptions.Length) return;
        enlargePanelImage.sprite = images[index].sprite;
        descriptionText.text = descriptions[index];
        enlargePanel.SetActive(true);
        enlargePanel.transform.localScale = Vector3.zero;

        CoroutineRunner.Instance.Run(AnimatePanelOpen(enlargePanel, 0.2f, originalScale));
    }

    public void CloseEnlargePanel()
    {
        if (enlargePanel != null)
        {
            CoroutineRunner.Instance.Run(AnimatePanelClose(enlargePanel, 0.2f));
        }
    }

    private IEnumerator AnimatePanelOpen(GameObject panel, float duration, Vector3 targetScale)
    {
        float elapsed = 0f;
        Vector3 startScale = panel.transform.localScale;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            panel.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
        panel.transform.localScale = targetScale;
    }

    private IEnumerator AnimatePanelClose(GameObject panel, float duration)
    {
        float elapsed = 0f;
        Vector3 startScale = panel.transform.localScale;
        Vector3 endScale = Vector3.zero;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            panel.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
        panel.transform.localScale = endScale;
        panel.SetActive(false);
    }
}
