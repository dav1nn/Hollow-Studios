using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginValidator : MonoBehaviour
{
    public TMP_InputField passwordField;
    public Button loginButton;
    public TextMeshProUGUI feedbackText; // Optional: For displaying "WRONG" feedback

    private string savedPassword;

    void Start()
    {
        loginButton.interactable = false;

        // Retrieve the saved password from PlayerPrefs
        savedPassword = PlayerPrefs.GetString("PlayerPassword", string.Empty);

        // Add listeners
        passwordField.onValueChanged.AddListener(ValidatePasswordInput);
        loginButton.onClick.AddListener(AttemptLogin);
    }

    void ValidatePasswordInput(string input)
    {
        // Enable button only if the password field has at least 4 characters
        loginButton.interactable = input.Length >= 4;
    }

    void AttemptLogin()
    {
        string enteredPassword = passwordField.text;

        if (enteredPassword == savedPassword)
        {
            // Password matches: Proceed to Desktop
            SceneManager.LoadScene("Pro 1");
        }
        else
        {
            // Password doesn't match: Show feedback
            StartCoroutine(ShowWrongPasswordFeedback());
        }
    }

    IEnumerator ShowWrongPasswordFeedback()
    {
        // Temporarily disable the login button
        loginButton.interactable = false;

        // Change button text and color
        feedbackText.text = "INCORRECT";
        loginButton.GetComponentInChildren<TextMeshProUGUI>().text = "INCORRECT";
        loginButton.GetComponent<Image>().color = Color.red;

        // Wait 2 seconds
        yield return new WaitForSeconds(2);

        // Reset button state
        feedbackText.text = ""; // Optional: Clear feedback text
        loginButton.GetComponentInChildren<TextMeshProUGUI>().text = "Login";
        loginButton.GetComponent<Image>().color = Color.white;
        passwordField.text = ""; // Clear the password field
    }
}
