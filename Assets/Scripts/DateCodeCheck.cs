using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DateCodeCheck : MonoBehaviour
{
    public TMP_InputField dayInput;
    public TMP_InputField monthInput;
    public TMP_InputField yearInput;
    public TMP_Text accessDeniedText;
    public GameObject currentGameObject;
    public GameObject nextGameObject;
    public Button nextButton;

    private Color originalButtonColor;
    private string originalButtonText;

    void Start()
    {
        originalButtonColor = nextButton.GetComponent<Image>().color;
        originalButtonText = nextButton.GetComponentInChildren<TMP_Text>().text;
        nextButton.interactable = false;
        dayInput.onValueChanged.AddListener(delegate { ValidateInputs(); });
        monthInput.onValueChanged.AddListener(delegate { ValidateInputs(); });
        yearInput.onValueChanged.AddListener(delegate { ValidateInputs(); });
    }

    void ValidateInputs()
    {
        nextButton.interactable = !string.IsNullOrEmpty(dayInput.text) &&
                                  !string.IsNullOrEmpty(monthInput.text) &&
                                  !string.IsNullOrEmpty(yearInput.text);
    }

    public void OnNextPressed()
    {
        if (dayInput.text == "27" && monthInput.text == "08" && yearInput.text == "1985")
        {
            StartCoroutine(GlitchAndDeactivate());
        }
        else
        {
            StartCoroutine(ShowErrorFeedback());
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
        ValidateInputs();
    }

    IEnumerator GlitchAndDeactivate()
    {
        Vector3 originalPos = currentGameObject.transform.localPosition;
        Vector3 originalScale = currentGameObject.transform.localScale;
        float glitchDuration = 1f;
        float elapsed = 0f;
        while (elapsed < glitchDuration)
        {
            float offsetX = Random.Range(-20f, 20f);
            float offsetY = Random.Range(-20f, 20f);
            currentGameObject.transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0f);
            yield return new WaitForSeconds(0.01f);
            currentGameObject.transform.localPosition = originalPos;
            elapsed += 0.01f;
        }
        float shrinkDuration = 0.1f;
        float shrinkElapsed = 0f;
        while (shrinkElapsed < shrinkDuration)
        {
            float t = shrinkElapsed / shrinkDuration;
            currentGameObject.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            shrinkElapsed += Time.deltaTime;
            yield return null;
        }
        currentGameObject.SetActive(false);
        currentGameObject.transform.localScale = originalScale;
        currentGameObject.transform.localPosition = originalPos;
        nextGameObject.SetActive(true);
    }
}
