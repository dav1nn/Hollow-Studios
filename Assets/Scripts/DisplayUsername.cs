using UnityEngine;
using TMPro;

public class DisplayUsername : MonoBehaviour
{
    public TextMeshProUGUI textField; 

    void Start()
    {

        string username = PlayerPrefs.GetString("PlayerUsername", "User");

        if (textField != null)
        {
            textField.text = textField.text.Replace("User", username);
        }
    }
}
