using UnityEngine;
using UnityEngine.UI;
using TMPro; 

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

    private void OnEnable()
    {
        PopulateGrid();
    }

    private void PopulateGrid()
    {
        foreach (Transform child in pictureGrid)
        {
            Destroy(child.gameObject);
        }

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
                if (button != null)
                {
                    button.onClick.AddListener(() => EnlargeImage(sprite, description));
                }
            }
        }
    }

    private void EnlargeImage(Sprite sprite, string description)
    {
        if (enlargePanel != null && enlargeImage != null && enlargeText != null)
        {
            enlargeImage.sprite = sprite;
            enlargeText.text = description; 
            enlargePanel.SetActive(true);
        }
    }

    public void CloseEnlargePanel()
    {
        if (enlargePanel != null)
        {
            enlargePanel.SetActive(false);
        }
    }

    public void CloseFolder()
    {
        if (folderPanel != null)
        {
            folderPanel.SetActive(false);
        }
    }
}
