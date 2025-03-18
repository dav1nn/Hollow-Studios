using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

    private Color originalButtonColor;
    private string originalButtonText;
    private int errorCount = 0;
    private static HashSet<GameObject> disabledObjects = new HashSet<GameObject>();

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

        foreach (GameObject obj in disabledObjects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
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
        if (input1.text == "88" && input2.text == "24" && input3.text == "1111")
        {
            input1.gameObject.SetActive(false);
            input2.gameObject.SetActive(false);
            input3.gameObject.SetActive(false);
            consoleResponse.gameObject.SetActive(false);

            foreach (GameObject obj in objectsToDisable)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                    disabledObjects.Add(obj);
                }
            }
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

    IEnumerator ShowErrorFeedback()
    {
        nextButton.GetComponentInChildren<TMP_Text>().text = "ERROR";
        nextButton.GetComponent<Image>().color = Color.red;
        accessDeniedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        nextButton.GetComponentInChildren<TMP_Text>().text = originalButtonText;
        nextButton.GetComponent<Image>().color = originalButtonColor;
        accessDeniedText.gameObject.SetActive(false);
    }

    IEnumerator FadeInText(TMP_Text text)
    {
        text.gameObject.SetActive(true);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        float elapsedTime = 0f;
        float duration = 1f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration);
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
}
