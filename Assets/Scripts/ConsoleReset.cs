using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ConsoleReset : MonoBehaviour
{
    public TMP_InputField consoleInputField;
    public TextMeshProUGUI consoleOutputText;
    public TextMeshProUGUI youOutputText;
    public Transform[] WindowsToReset;

    private List<string> numberSequence = new List<string>();
    private bool isTyping = false;

    public float shakeIntensity = 2f;
    public float shakeSpeed = 50f;
    private TMP_TextInfo textInfo;
    private List<int> shakyIndices = new List<int>();
    private int maxLines = 10;
    private int maxCharactersPerLine = 30;

    public Image shutdownEffectImage;
    public float fadeDuration = 2f;
    public string nextScene = "3D";

    void Start()
    {
        if (consoleOutputText != null)
            consoleOutputText.text = "";
        if (youOutputText != null)
        {
            youOutputText.text = "";
            youOutputText.gameObject.SetActive(false);
        }
        if (shutdownEffectImage != null)
        {
            Color c = shutdownEffectImage.color;
            c.a = 0f;
            shutdownEffectImage.color = c;
            shutdownEffectImage.gameObject.SetActive(false);
        }
    }

    public void OnInputSubmit()
    {
        string userInput = consoleInputField.text.Trim();
        if (!string.IsNullOrEmpty(userInput))
        {
            if (userInput.Equals("reset", System.StringComparison.OrdinalIgnoreCase))
            {
                ResetConsole();
            }
            else if (userInput.Equals("YOU", System.StringComparison.OrdinalIgnoreCase))
            {
                if (!isTyping)
                {
                    consoleOutputText.text = "";
                    StartCoroutine(TypeNumbersLoop());
                }
            }
            else if (userInput.Equals("ENTERTHEVOID.EXE", System.StringComparison.OrdinalIgnoreCase))
            {
                StartCoroutine(HandleShutdownEffect());
            }
            else
            {
                consoleOutputText.text = "Unknown command: " + userInput;
            }
        }
        consoleInputField.text = string.Empty;
    }

    void ResetConsole()
    {
        youOutputText.text = "";
        youOutputText.gameObject.SetActive(false);
        consoleOutputText.text = "Console has been reset.";
        isTyping = false;
        StopAllCoroutines();
    }

    void GenerateNumberSequence()
    {
        numberSequence.Clear();
        List<string> baseNumbers = new List<string> { "88", "1111", "24" };
        for (int i = 0; i < 80; i++)
        {
            int randomValue = Random.Range(0, 2) == 0 ? Random.Range(10, 100) : Random.Range(1000, 10000);
            numberSequence.Add(randomValue.ToString());
            if (Random.Range(0, 5) == 0)
            {
                numberSequence.Add(baseNumbers[Random.Range(0, baseNumbers.Count)]);
            }
        }
    }

    IEnumerator TypeNumbersLoop()
    {
        isTyping = true;
        youOutputText.gameObject.SetActive(true);
        while (true)
        {
            GenerateNumberSequence();
            youOutputText.text = "";
            shakyIndices.Clear();
            int lineCount = 0;
            int currentLineLength = 0;
            foreach (string num in numberSequence)
            {
                if (lineCount >= maxLines)
                    break;
                if (currentLineLength + num.Length + 1 > maxCharactersPerLine)
                {
                    youOutputText.text += "\n";
                    lineCount++;
                    currentLineLength = 0;
                    if (lineCount >= maxLines)
                        break;
                }
                int startIndex = youOutputText.text.Length;
                youOutputText.text += num + " ";
                currentLineLength += num.Length + 1;
                if (num == "88" || num == "1111" || num == "24")
                {
                    for (int i = 0; i < num.Length; i++)
                    {
                        shakyIndices.Add(startIndex + i);
                    }
                }
                yield return new WaitForSeconds(0.02f);
            }
            StartCoroutine(ApplyShakyEffect());
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator ApplyShakyEffect()
    {
        while (true)
        {
            if (youOutputText == null)
                yield break;
            youOutputText.ForceMeshUpdate();
            textInfo = youOutputText.textInfo;
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible)
                    continue;
                if (shakyIndices.Contains(i))
                {
                    var meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];
                    Vector3[] vertices = meshInfo.vertices;
                    int vertexIndex = charInfo.vertexIndex;
                    for (int j = 0; j < 4; j++)
                    {
                        Vector3 offset = new Vector3(
                            Mathf.Sin(Time.time * shakeSpeed + j) * shakeIntensity,
                            Mathf.Cos(Time.time * shakeSpeed + j) * shakeIntensity,
                            0);
                        vertices[vertexIndex + j] += offset;
                    }
                }
            }
            youOutputText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
            yield return null;
        }
    }

    IEnumerator HandleShutdownEffect()
    {
        if (shutdownEffectImage == null)
            yield break;
        shutdownEffectImage.gameObject.SetActive(true);
        float timer = 0f;
        Color c = shutdownEffectImage.color;
        c.a = 0f;
        shutdownEffectImage.color = c;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            c.a = Mathf.Lerp(0f, 1f, t);
            shutdownEffectImage.color = c;
            yield return null;
        }
        c.a = 1f;
        shutdownEffectImage.color = c;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextScene);
    }
}