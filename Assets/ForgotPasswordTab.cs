using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForgotPasswordTab : MonoBehaviour
{
    public GameObject forgotPasswordPanel;
    public TMP_InputField newPasswordField;
    public TMP_InputField confirmPasswordField;
    public Button confirmButton;
    public Button cancelButton;

    void Start()
    {
        forgotPasswordPanel.SetActive(false);
        confirmButton.interactable = false;

        confirmPasswordField.onValueChanged.AddListener(ValidateMatchingPasswords);
        newPasswordField.onValueChanged.AddListener(ValidateMatchingPasswords);
        confirmButton.onClick.AddListener(ConfirmNewPassword);
        cancelButton.onClick.AddListener(CloseTab);
    }

    public void OpenTab()
    {
        forgotPasswordPanel.SetActive(true);
        newPasswordField.text = "";
        confirmPasswordField.text = "";
        confirmButton.interactable = false;
    }

    void ValidateMatchingPasswords(string _)
    {
        confirmButton.interactable =
            !string.IsNullOrEmpty(newPasswordField.text) &&
            newPasswordField.text == confirmPasswordField.text;
    }

    void ConfirmNewPassword()
    {
        PlayerPrefs.SetString("PlayerPassword", newPasswordField.text);
        PlayerPrefs.Save();
        CloseTab();
    }

    void CloseTab()
    {
        forgotPasswordPanel.SetActive(false);
    }
}
