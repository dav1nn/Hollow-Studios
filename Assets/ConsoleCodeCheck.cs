using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ConsoleCodeCheck : MonoBehaviour
{
    public TMP_InputField input1;
    public TMP_InputField input2;
    public TMP_InputField input3;
    public TMP_Text accessDeniedText;
    public TMP_Text warningText;
    public TMP_Text extraWarningText;
    public TMP_Text finalWarningText;
    public GameObject currentGameObject;
    public Button nextButton;
    public TMP_Text consoleResponse;
    public List<GameObject> objectsToDisable;
    public List<TMP_Text> noahConsoleMessages;
    public TMP_InputField consoleInput;
    public TMP_Text textToHide;
    public TMP_Text textToFadeIn;
    public Image fadeImage;
    public float textFadeSpeed = 1f;
    public float fadeDuration = 2f;

    private Color originalButtonColor;
    private string originalButtonText;
    private int errorCount = 0;
    private static HashSet<GameObject> disabledObjects = new HashSet<GameObject>();
    private int messageIndex = 0;
    private bool displayingText = false;
    private bool codeAccepted = false;
    private bool transitionStarted = false;

    void Start()
    {
        originalButtonColor = nextButton.GetComponent<Image>().color;
        originalButtonText = nextButton.GetComponentInChildren<TMP_Text>().text;
        nextButton.interactable = false;
        input1.onValueChanged.AddListener(delegate { ValidateInputs(); });
        input2.onValueChanged.AddListener(delegate { ValidateInputs(); });
        input3.onValueChanged.AddListener(delegate { ValidateInputs(); });
        warningText.gameObject.SetActive(false);
        extraWarningText.gameObject.SetActive(false);
        finalWarningText.gameObject.SetActive(false);
        textToFadeIn.gameObject.SetActive(false);
        fadeImage.gameObject.SetActive(false);
        foreach (GameObject obj in disabledObjects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
        foreach (TMP_Text message in noahConsoleMessages)
        {
            message.gameObject.SetActive(false);
        }
    }

    void ValidateInputs()
    {
        nextButton.interactable = !string.IsNullOrEmpty(input1.text) &&
                                  !string.IsNullOrEmpty(input2.text) &&
                                  !string.IsNullOrEmpty(input3.text);
    }

    public void OnNextPressed()
    {
        if (!codeAccepted)
        {
            if (input1.text == "88" && input2.text == "24" && input3.text == "1111")
            {
                input1.gameObject.SetActive(false);
                input2.gameObject.SetActive(false);
                input3.gameObject.SetActive(false);
                consoleResponse.gameObject.SetActive(false);
                consoleInput.gameObject.SetActive(false);
                codeAccepted = true;
                warningText.gameObject.SetActive(false);
                extraWarningText.gameObject.SetActive(false);
                finalWarningText.gameObject.SetActive(false);
                nextButton.GetComponentInChildren<TMP_Text>().text = "CONTINUE";
                textToHide.gameObject.SetActive(false);
                StartCoroutine(FadeInText(textToFadeIn));
                foreach (GameObject obj in objectsToDisable)
                {
                    if (obj != null)
                    {
                        obj.SetActive(false);
                        disabledObjects.Add(obj);
                    }
                }
                DisplayNextMessage();
            }
            else
            {
                errorCount++;
                if (errorCount == 3)
                {
                    StartCoroutine(FadeInText(warningText));
                }
                if (errorCount == 8)
                {
                    StartCoroutine(FadeInText(extraWarningText));
                }
                if (errorCount == 14)
                {
                    StartCoroutine(FadeInText(finalWarningText));
                }
                StartCoroutine(ShowErrorFeedback());
            }
        }
        else
        {
            DisplayNextMessage();
        }
    }

    IEnumerator ShowErrorFeedback()
    {
        nextButton.interactable = false;
        nextButton.GetComponentInChildren<TMP_Text>().text = "ERROR";
        nextButton.GetComponent<Image>().color = Color.red;
        accessDeniedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        nextButton.GetComponentInChildren<TMP_Text>().text = originalButtonText;
        nextButton.GetComponent<Image>().color = originalButtonColor;
        accessDeniedText.gameObject.SetActive(false);
        nextButton.interactable = true;
    }

    IEnumerator FadeInText(TMP_Text text)
    {
        text.gameObject.SetActive(true);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        float elapsedTime = 0f;
        while (elapsedTime < textFadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / textFadeSpeed);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }
    }

    void Update()
    {
        foreach (GameObject obj in disabledObjects)
        {
            if (obj != null && obj.activeSelf)
            {
                obj.SetActive(false);
            }
        }
    }

    void DisplayNextMessage()
    {
        if (messageIndex < noahConsoleMessages.Count)
        {
            if (messageIndex > 0)
            {
                noahConsoleMessages[messageIndex - 1].gameObject.SetActive(false);
            }
            nextButton.interactable = false;
            StartCoroutine(TypeMessage(noahConsoleMessages[messageIndex]));
            messageIndex++;
        }
        else if (!transitionStarted)
        {
            transitionStarted = true;
            StartCoroutine(FadeToBlackAndLoadScene());
        }
    }

    IEnumerator TypeMessage(TMP_Text message)
    {
        displayingText = true;
        message.gameObject.SetActive(true);
        string fullText = message.text;
        message.text = "";
        for (int i = 0; i < fullText.Length; i++)
        {
            message.text += fullText[i];
            yield return new WaitForSeconds(0.05f);
        }
        displayingText = false;
        if (messageIndex == noahConsoleMessages.Count && !transitionStarted)
        {
            transitionStarted = true;
            yield return new WaitForSeconds(5);
            StartCoroutine(FadeToBlackAndLoadScene());
        }
        else
        {
            nextButton.interactable = true;
        }
    }

    IEnumerator FadeToBlackAndLoadScene()
    {
        fadeImage.gameObject.SetActive(true);
        Color c = fadeImage.color;
        c.a = 0;
        fadeImage.color = c;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
