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
            currentGameObject.SetActive(false);
            nextGameObject.SetActive(true);
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
}