using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Register2Manager : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;
    public TMP_Dropdown colorDropdown;  
    public TMP_Dropdown animalDropdown; 
    public Button confirmButton;       

    void Start()
    {
  
        string username = PlayerPrefs.GetString("PlayerUsername", "User");
        welcomeText.text = $"Hi, {username}. You're almost ready.";

        confirmButton.onClick.AddListener(SavePreferences);
    }

    void SavePreferences()
    {
        string favoriteColor = colorDropdown.options[colorDropdown.value].text;
        string favoriteAnimal = animalDropdown.options[animalDropdown.value].text;

        PlayerPrefs.SetString("FavoriteColor", favoriteColor);
        PlayerPrefs.SetString("FavoriteAnimal", favoriteAnimal);
        PlayerPrefs.Save();

        Debug.Log($"Preferences Saved: Color={favoriteColor}, Animal={favoriteAnimal}");
    }
}
