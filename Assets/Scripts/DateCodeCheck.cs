using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateCodeCheck : MonoBehaviour
{
    public TMP_InputField dayInput;
    public TMP_InputField monthInput;
    public TMP_InputField yearInput;
    public TMP_Text accessDeniedText;
    public TMP_Text warningText;
    public TMP_Text extraWarningText;
    public TMP_Text finalWarningText;
    public GameObject currentGameObject;
    public GameObject nextGameObject;
    public Button nextButton;
    public AudioSource glitchSound;
    public AudioSource typingSound;

    private Color originalButtonColor;
    private string originalButtonText;
    private int errorCount = 0;
    private float lastPlayTime = 0f;
    private float typeSoundCooldown = 0.05f;

    void Start()
    {
        originalButtonColor = nextButton.GetComponent<Image>().color;
        originalButtonText = nextButton.GetComponentInChildren<TMP_Text>().text;
        nextButton.interactable = false;

        dayInput.onValueChanged.AddListener(delegate { PlayTypingSound(); ValidateInputs(); });
        monthInput.onValueChanged.AddListener(delegate { PlayTypingSound(); ValidateInputs(); });
        yearInput.onValueChanged.AddListener(delegate { PlayTypingSound(); ValidateInputs(); });

        warningText.gameObject.SetActive(false);
        extraWarningText.gameObject.SetActive(false);
        finalWarningText.gameObject.SetActive(false);
    }

    void PlayTypingSound()
    {
        if (typingSound != null && Time.time - lastPlayTime > typeSoundCooldown)
        {
            typingSound.PlayOneShot(typingSound.clip);
            lastPlayTime = Time.time;
        }
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
            errorCount++;
            if (errorCount == 3) StartCoroutine(FadeInText(warningText));
            if (errorCount == 8) StartCoroutine(FadeInText(extraWarningText));
            if (errorCount == 14) StartCoroutine(FadeInText(finalWarningText));
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

        if (glitchSound != null && !glitchSound.isPlaying)
        {
            glitchSound.Play();
        }

        while (elapsed < glitchDuration)
        {
            float offsetX = Random.Range(-20f, 20f);
            float offsetY = Random.Range(-20f, 20f);
            currentGameObject.transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0f);
            yield return new WaitForSeconds(0.01f);
            elapsed += 0.01f;
        }

        if (glitchSound != null && glitchSound.isPlaying)
        {
            glitchSound.Stop();
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
}
