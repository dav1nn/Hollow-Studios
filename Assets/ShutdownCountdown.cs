using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShutdownCountdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI[] fadingTexts;
    public TextMeshProUGUI[] restartPromptTexts;

    private bool canRestart = false;

    private void Start()
    {
        foreach (var text in restartPromptTexts)
        {
            text.gameObject.SetActive(false);
        }

        StartCoroutine(StartShutdownSequence());
    }

    private void Update()
    {
        if (canRestart && Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Intro");
        }
    }

    IEnumerator StartShutdownSequence()
    {
        yield return new WaitForSeconds(40f);

        countdownText.gameObject.SetActive(true);

        for (int i = 10; i > 0; i--)
        {
            countdownText.text = "SHUTTING DOWN - " + i;
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(FadeOutTextsAndShowRestartPrompt());
    }

    IEnumerator FadeOutTextsAndShowRestartPrompt()
    {
        float duration = 1f;
        float elapsed = 0f;

        float[] originalAlphas = new float[fadingTexts.Length];
        for (int i = 0; i < fadingTexts.Length; i++)
        {
            originalAlphas[i] = fadingTexts[i].color.a;
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = 1 - (elapsed / duration);

            for (int i = 0; i < fadingTexts.Length; i++)
            {
                Color c = fadingTexts[i].color;
                c.a = Mathf.Lerp(0, originalAlphas[i], t);
                fadingTexts[i].color = c;
            }

            yield return null;
        }

        foreach (var text in fadingTexts)
        {
            text.gameObject.SetActive(false);
        }

        countdownText.gameObject.SetActive(false);

        foreach (var text in restartPromptTexts)
        {
            text.gameObject.SetActive(true);
        }

        canRestart = true;
    }
}
