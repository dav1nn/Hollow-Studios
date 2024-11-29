using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class FormValidator : MonoBehaviour
{
    public TMP_InputField usernameField; 
    public TMP_InputField passwordField; 
    public Button nextButton;

    void Start()
    {
        nextButton.interactable = false;

        usernameField.onValueChanged.AddListener(delegate { ValidateForm(); });
        passwordField.onValueChanged.AddListener(delegate { ValidateForm(); });
    }

    void ValidateForm()
    {
        bool isUsernameValid = !string.IsNullOrEmpty(usernameField.text);
        bool isPasswordValid = !string.IsNullOrEmpty(passwordField.text) && passwordField.text.Length >= 4;

        nextButton.interactable = isUsernameValid && isPasswordValid;
    }
}
