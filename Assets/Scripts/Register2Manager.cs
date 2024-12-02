using UnityEngine;
using TMPro;

public class Register2Manager : MonoBehaviour
{
    public TextMeshProUGUI welcomeText; 

    void Start()
    {
        string username = PlayerPrefs.GetString("PlayerUsername", "Player"); 

        welcomeText.text = $"Hello, {username}!";
    }
}
