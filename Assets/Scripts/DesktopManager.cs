using UnityEngine;
using UnityEngine.UI;

public class DesktopManager : MonoBehaviour
{
    public Image profilePictureDisplay; 
    public Sprite[] profileSprites;    

    void Start()
    {
        int selectedPictureIndex = PlayerPrefs.GetInt("SelectedProfilePicture", -1);

        if (selectedPictureIndex >= 0 && selectedPictureIndex < profileSprites.Length)
        {
            profilePictureDisplay.sprite = profileSprites[selectedPictureIndex];
            Debug.Log($"Profile picture {selectedPictureIndex} displayed.");
        }
        else
        {
            Debug.LogWarning("No valid profile picture selected.");
        }
    }
}
