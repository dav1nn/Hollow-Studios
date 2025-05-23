using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CoroutineRunner : MonoBehaviour
{
    static CoroutineRunner instance;
    public static CoroutineRunner Instance
    {
        get
        {
            if (!instance)
            {
                GameObject o = new GameObject("CoroutineRunner");
                DontDestroyOnLoad(o);
                instance = o.AddComponent<CoroutineRunner>();
            }
            return instance;
        }
    }
    public Coroutine Run(IEnumerator routine) { return StartCoroutine(routine); }
}

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
    private Vector3 folderPanelOriginalScale;

    private void Awake()
    {
        if (enlargePanel != null) enlargePanelOriginalScale = enlargePanel.transform.localScale;
        if (folderPanel != null) folderPanelOriginalScale = folderPanel.transform.localScale;
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
            Sprite s = pictureSprites[i];
            string d = i < pictureDescriptions.Length ? pictureDescriptions[i] : "No description available";
            GameObject n = Instantiate(picturePrefab, pictureGrid);
            Image img = n.GetComponent<Image>();
            if (img != null)
            {
                img.sprite = s;
                Button b = n.GetComponent<Button>();
                if (b != null) b.onClick.AddListener(() => EnlargeImage(s, d));
            }
        }
    }

    private void EnlargeImage(Sprite s, string d)
    {
        if (enlargePanel != null && enlargeImage != null && enlargeText != null)
        {
            enlargeImage.sprite = s;
            enlargeText.text = d;
            enlargePanel.SetActive(true);
            enlargePanel.transform.localScale = Vector3.zero;
            CoroutineRunner.Instance.Run(AnimatePanelOpen(enlargePanel, 0.2f, enlargePanelOriginalScale));
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
        if (enlargePanel != null) CoroutineRunner.Instance.Run(AnimatePanelClose(enlargePanel, 0.2f));
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

    public void CloseFolder()
    {
        if (folderPanel != null) CoroutineRunner.Instance.Run(AnimateFolderClose(folderPanel, 0.2f));
    }

    private IEnumerator AnimateFolderClose(GameObject panel, float duration)
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
        panel.transform.localScale = folderPanelOriginalScale;
    }
}
