using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureFolder : MonoBehaviour
{
    [SerializeField] private GameObject picturePrefab; 
    [SerializeField] private Transform pictureGrid; 
    [SerializeField] private Sprite[] pictureSprites; 

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
            }
        }
    }
}

