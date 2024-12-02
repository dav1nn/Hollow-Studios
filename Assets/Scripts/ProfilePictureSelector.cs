using UnityEngine;
using UnityEngine.UI;

public class ProfilePictureSelector : MonoBehaviour
{
    public Image[] profilePictures; 
    public Sprite[] profileSprites; 
    public Image selectedHighlight; 

    private int selectedPictureIndex = -1;

    void Start()
    {
        for (int i = 0; i < profilePictures.Length; i++)
        {
            int index = i; 
            profilePictures[i].GetComponent<Button>().onClick.AddListener(() => SelectProfilePicture(index));
        }
    }

    public void SelectProfilePicture(int index)
    {
        selectedPictureIndex = index;

        selectedHighlight.transform.position = profilePictures[index].transform.position;

        Debug.Log($"Profile picture {index} selected.");
    }

    public void SaveProfileSelection()
    {
        if (selectedPictureIndex >= 0)
        {
            PlayerPrefs.SetInt("SelectedProfilePicture", selectedPictureIndex);
            PlayerPrefs.Save();

            Debug.Log($"Profile picture {selectedPictureIndex} saved.");
        }
        else
        {
            Debug.LogWarning("No profile picture selected.");
        }
    }
}
