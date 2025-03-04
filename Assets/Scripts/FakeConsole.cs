using UnityEngine;
using TMPro;

public class FakeConsole : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField consoleInputField;
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI consoleOutput;

    [Header("Other References")]
    public ResetObjectPositions resetScript;

    private string playerName;

    private void Start()
    {
        playerName = PlayerPrefs.GetString("PlayerUsername", "User");
        if (promptText)
        {
            promptText.text = $"C:\\User\\{playerName}>";
        }
        if (consoleOutput)
        {
            consoleOutput.text = "";
        }
        if (consoleInputField)
        {
            consoleInputField.onSubmit.AddListener(HandleInput);
        }
    }

    private void HandleInput(string inputValue)
    {
        if (string.IsNullOrWhiteSpace(inputValue))
        {
            return;
        }
        LogToConsole($"{promptText.text} {inputValue}");
        if (inputValue.ToLower().Trim() == "reset")
        {
            if (resetScript)
                resetScript.ResetPositions();
            LogToConsole("Program Positions have been reset.");
        }
        else
        {
            LogToConsole("Unknown command");
        }
        consoleInputField.text = "";
        consoleInputField.ActivateInputField();
    }

    private void LogToConsole(string message)
    {
        if (consoleOutput)
        {
            consoleOutput.text += message + "\n";
        }
        else
        {
            Debug.Log(message);
        }
    }
}
