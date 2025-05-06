using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginValidator : MonoBehaviour
{
    public TMP_InputField passwordField;
    public Button loginButton;
    public TextMeshProUGUI feedbackText;
    public Button forgotPasswordButton;

    private string savedPassword;
    private int failedAttempts = 0;

    void Start()
    {
        loginButton.interactable = false;
        forgotPasswordButton.interactable = false;

        savedPassword = PlayerPrefs.GetString("PlayerPassword", string.Empty);

        passwordField.onValueChanged.AddListener(ValidatePasswordInput);
        loginButton.onClick.AddListener(AttemptLogin);
    }

    void ValidatePasswordInput(string input)
    {
        loginButton.interactable = input.Length >= 4;
    }

    void AttemptLogin()
    {
        string enteredPassword = passwordField.text;

        if (enteredPassword == savedPassword)
        {
            SceneManager.LoadScene("Pro 1");
        }
        else
        {
            failedAttempts++;

            if (failedAttempts >= 3)
            {
                forgotPasswordButton.interactable = true;
            }

            StartCoroutine(ShowWrongPasswordFeedback());
        }
    }

    IEnumerator ShowWrongPasswordFeedback()
    {
        loginButton.interactable = false;

        feedbackText.text = "INCORRECT";
        loginButton.GetComponentInChildren<TextMeshProUGUI>().text = "INCORRECT";
        loginButton.GetComponent<Image>().color = Color.red;

        yield return new WaitForSeconds(2);

        feedbackText.text = "";
        loginButton.GetComponentInChildren<TextMeshProUGUI>().text = "Login";
        loginButton.GetComponent<Image>().color = Color.white;
        passwordField.text = "";
    }
}
