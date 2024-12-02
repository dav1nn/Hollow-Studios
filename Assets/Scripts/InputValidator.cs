using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputValidator : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public Button nextButton;

    void Start()
    {
        nextButton.interactable = false;

        usernameField.onValueChanged.AddListener(ValidateUsername);
        passwordField.onValueChanged.AddListener(ValidatePassword);

        nextButton.onClick.AddListener(LoadNextScene);
    }

    void ValidateUsername(string input)
    {
        if (input.Length > 20)
        {
            usernameField.text = input.Substring(0, 20);
        }

        ValidateForm();
    }

    void ValidatePassword(string input)
    {
        if (input.Length > 20)
        {
            passwordField.text = input.Substring(0, 20);
        }

        ValidateForm();
    }

    void ValidateForm()
    {
        bool isUsernameValid = !string.IsNullOrEmpty(usernameField.text);
        bool isPasswordValid = passwordField.text.Length >= 4;

        nextButton.interactable = isUsernameValid && isPasswordValid;
    }

    void LoadNextScene()
    {
        PlayerPrefs.SetString("PlayerUsername", usernameField.text);
        PlayerPrefs.SetString("PlayerPassword", passwordField.text); 
        PlayerPrefs.Save();

        SceneManager.LoadScene("Register2");
    }
}
