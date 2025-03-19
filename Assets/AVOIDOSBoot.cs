using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AVOIDOSBoot : MonoBehaviour
{
    public TextMeshProUGUI bootText;
    public float minDelay = 0.2f;
    public float maxDelay = 2.0f;
    private int finalMemory = 65536;

    private string[] staticLines = new string[]
    {
        "aVoid OS v3.02 - System Initialization",
        "Copyright (C) 1998-2025, naafiri Systems",
        "",
        "Initializing Core Modules...",
        "Checking Hardware Configuration...",
        "CPU: VX-Engine @ 512MHz",
        "Network Status: CONNECTED",
        "Executing Startup Commands...",
        "Performing Final System Checks...",
        "Booting into aVoid OS..."
    };

    private string[] dynamicLines = new string[]
    {
        "Storage Device Detected: NODE-47",
        "User Profile Found: Noah",
        "Verifying System Integrity...",
        "AUTH_KEY: LeviA-1304",
        "Process ID: 8729-VOID"
    };

    private string[] updatingLines = new string[]
    {
        "Checking system bus...",
        "Configuring hardware interrupts...",
        "Loading device drivers...",
        "Initializing virtual partitions...",
        "Applying network policies...",
        "System time synchronized.",
        "System diagnostics complete."
    };

    private bool isBlinking = true;

    void Start()
    {
        bootText.text = "";
        StartCoroutine(DisplayBootSequence());
        StartCoroutine(BlinkingCursor());
    }

    IEnumerator DisplayBootSequence()
    {
        foreach (string line in staticLines)
        {
            yield return StartCoroutine(ShowLine(line, Random.Range(minDelay, maxDelay)));
        }

        yield return StartCoroutine(DisplayMemoryCheck());

        foreach (string line in dynamicLines)
        {
            yield return StartCoroutine(ShowLine(line, Random.Range(minDelay, maxDelay)));
        }

        yield return StartCoroutine(PerformUpdatingLines());

        yield return new WaitForSeconds(1);
        bootText.text += "System Ready.\n_";
        isBlinking = false;

        yield return new WaitForSeconds(1f);
        LoadNextScene();
    }

    IEnumerator DisplayMemoryCheck()
    {
        string baseText = bootText.text + "Memory Check: ";
        for (int i = 0; i < 25; i++)
        {
            int randomMemory = Random.Range(32000, 66000);
            bootText.text = baseText + randomMemory + "K\n_";
            yield return new WaitForSeconds(0.05f);
        }
        bootText.text = baseText + $"{finalMemory}K OK\n_";
    }

    IEnumerator PerformUpdatingLines()
    {
        string baseText = bootText.text;
        for (int i = 0; i < updatingLines.Length; i++)
        {
            bootText.text = baseText + updatingLines[i] + "\n_";
            yield return new WaitForSeconds(Random.Range(minDelay * 0.5f, maxDelay * 0.5f));
            baseText = bootText.text;
        }
    }

    IEnumerator ShowLine(string line, float delay)
    {
        bootText.text = bootText.text.TrimEnd('_') + line + "\n_";
        yield return new WaitForSeconds(delay);
    }

    IEnumerator BlinkingCursor()
    {
        while (isBlinking)
        {
            yield return new WaitForSeconds(0.5f);
            bootText.text = bootText.text.TrimEnd('_');
            yield return new WaitForSeconds(0.5f);
            bootText.text += "_";
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
