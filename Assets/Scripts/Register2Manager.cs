using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Register2Manager : MonoBehaviour
{
    public Button nextButton;
    public Image[] profilePictures; 
    private int selectedPictureIndex = -1; 

    void Start()
    {
        nextButton.interactable = false;

        for (int i = 0; i < profilePictures.Length; i++)
        {
            int index = i; 
            profilePictures[i].GetComponent<Button>().onClick.AddListener(() => SelectProfilePicture(index));
        }

        nextButton.onClick.AddListener(GoToDesktopScene);
    }

    void SelectProfilePicture(int index)
    {
        selectedPictureIndex = index;

        nextButton.interactable = true;

        HighlightSelectedPicture(index);

        Debug.Log($"Profile picture {index} selected.");
    }

    void HighlightSelectedPicture(int index)
    {
 
        foreach (var picture in profilePictures)
        {
            picture.color = Color.white; 
        }

        profilePictures[index].color = Color.green;
    }

    void GoToDesktopScene()
    {
        if (selectedPictureIndex >= 0)
        {

            PlayerPrefs.SetInt("SelectedProfilePicture", selectedPictureIndex);
            PlayerPrefs.Save();

            SceneManager.LoadScene("Login");
        }
        else
        {
            Debug.LogWarning("No profile picture selected.");
        }
    }
}
