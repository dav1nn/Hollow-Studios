using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PictureFolder : MonoBehaviour
{
    [SerializeField] private GameObject picturePrefab;
    [SerializeField] private Transform pictureGrid;
    [SerializeField] private Sprite[] pictureSprites;
    [SerializeField] private string[] pictureDescriptions;
    [SerializeField] private GameObject enlargePanel;
    [SerializeField] private Image enlargeImage;
    [SerializeField] private TextMeshProUGUI enlargeText;
    [SerializeField] private GameObject folderPanel;

    private Vector3 enlargePanelOriginalScale;

    private void Awake()
    {
        if (enlargePanel != null) enlargePanelOriginalScale = enlargePanel.transform.localScale;
    }

    private void OnEnable()
    {
        PopulateGrid();
    }

    private void PopulateGrid()
    {
        foreach (Transform child in pictureGrid) Destroy(child.gameObject);
        for (int i = 0; i < pictureSprites.Length; i++)
        {
            Sprite sprite = pictureSprites[i];
            string description = i < pictureDescriptions.Length ? pictureDescriptions[i] : "No description available";
            GameObject newPicture = Instantiate(picturePrefab, pictureGrid);
            Image imageComponent = newPicture.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = sprite;
                Button button = newPicture.GetComponent<Button>();
                if (button != null) button.onClick.AddListener(() => EnlargeImage(sprite, description));
            }
        }
    }

    private void EnlargeImage(Sprite sprite, string description)
    {
        if (enlargePanel != null && enlargeImage != null && enlargeText != null)
        {
            enlargeImage.sprite = sprite;
            enlargeText.text = description;
            enlargePanel.transform.localScale = Vector3.zero;
            enlargePanel.SetActive(true);
            StartCoroutine(AnimatePanelOpen(enlargePanel, 0.2f, enlargePanelOriginalScale));
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

    public void CloseEnlargePanel()
    {
        if (enlargePanel != null) enlargePanel.SetActive(false);
    }

    public void CloseFolder()
    {
        if (folderPanel != null) folderPanel.SetActive(false);
    }
}

