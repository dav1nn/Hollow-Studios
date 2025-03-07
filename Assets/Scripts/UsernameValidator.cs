using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UsernameValidator : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TextMeshProUGUI feedbackText;
    public Button nextButton;

    private string systemUsername;
    private Coroutine typewriterCoroutine;
    private int invalidAttempts = 0;
    private bool isFinalMessageDisplayed = false; 

    void Start()
    {
        systemUsername = System.Environment.UserName;
        systemUsername = CapitalizeFirstLetter(systemUsername);
        feedbackText.text = "";
        feedbackText.gameObject.SetActive(false);

        nextButton.onClick.AddListener(ValidateAndProceed);
    }

    void ValidateAndProceed()
    {
        
        if (isFinalMessageDisplayed)
        {
            return;
        }

        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        string enteredUsername = usernameField.text;

        if (!string.IsNullOrEmpty(enteredUsername) && !string.Equals(enteredUsername, systemUsername, System.StringComparison.OrdinalIgnoreCase))
        {
            invalidAttempts++;

            
            if (invalidAttempts >= 4)
            {
                string finalMessage = "Fine. Do whatever you want.";
                feedbackText.gameObject.SetActive(true);
                isFinalMessageDisplayed = true; 
                typewriterCoroutine = StartCoroutine(TypewriterEffect(finalMessage));
                StartCoroutine(AllowSceneChangeAfterDelay(2f)); 
            }
            else
            {
                string feedbackMessage = GetEscalatedFeedbackMessage();
                feedbackText.gameObject.SetActive(true);
                typewriterCoroutine = StartCoroutine(TypewriterEffect(feedbackMessage));
            }
        }
        else
        {
            feedbackText.text = "";
            feedbackText.gameObject.SetActive(false);
            LoadNextScene();
        }
    }

    string GetEscalatedFeedbackMessage()
    {
        switch (invalidAttempts)
        {
            case 1:
                return $"Why not use a better name? Like this: {systemUsername}";
            case 2:
                return $"I really think {systemUsername} suits you better.";
            case 3:
                return $"{systemUsername} is your real name. Why are you avoiding it?";
            default:
                return $"Why not use a better name? Like this: {systemUsername}";
        }
    }

    IEnumerator TypewriterEffect(string message)
    {
        feedbackText.text = "";
        foreach (char c in message)
        {
            feedbackText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator AllowSceneChangeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        string enteredUsername = usernameField.text;

        PlayerPrefs.SetString("PlayerUsername", enteredUsername);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Register2");
    }

    private string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToUpper(input[0]) + input.Substring(1).ToLower();
    }
}
