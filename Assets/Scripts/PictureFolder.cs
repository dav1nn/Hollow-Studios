using UnityEngine;
using UnityEngine.UI;

public class PictureFolder : MonoBehaviour
{
    [SerializeField] private GameObject picturePrefab; 
    [SerializeField] private Transform pictureGrid; 
    [SerializeField] private Sprite[] pictureSprites; 
    [SerializeField] private GameObject enlargePanel; 
    [SerializeField] private Image enlargeImage;
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

        
        foreach (Sprite sprite in pictureSprites)
        {
            GameObject newPicture = Instantiate(picturePrefab, pictureGrid);
            Image imageComponent = newPicture.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = sprite;

                
                Button button = newPicture.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() => EnlargeImage(sprite));
                }
            }
        }
    }

    private void EnlargeImage(Sprite sprite)
    {
        if (enlargePanel != null && enlargeImage != null)
        {
            enlargeImage.sprite = sprite;
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



