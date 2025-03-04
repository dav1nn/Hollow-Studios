using UnityEngine;
using TMPro;

public class ConsoleReset : MonoBehaviour
{
    [Header("Input / Output")]
    public TMP_InputField consoleInputField;
    public TextMeshProUGUI consoleOutputText;

    [Header("Windows to Reset")]
    public Transform[] WindowsToReset;

    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;
    private Vector3[] initialScales;

    void Start()
    {
        initialPositions = new Vector3[WindowsToReset.Length];
        initialRotations = new Quaternion[WindowsToReset.Length];
        initialScales = new Vector3[WindowsToReset.Length];

        for (int i = 0; i < WindowsToReset.Length; i++)
        {
            initialPositions[i] = WindowsToReset[i].localPosition;
            initialRotations[i] = WindowsToReset[i].localRotation;
            initialScales[i] = WindowsToReset[i].localScale;
        }

        if (consoleOutputText != null)
        {
            consoleOutputText.text = "";
        }
    }

    public void OnInputSubmit()
    {
        string userInput = consoleInputField.text.Trim();

        if (!string.IsNullOrEmpty(userInput))
        {
            if (userInput.Equals("reset", System.StringComparison.OrdinalIgnoreCase))
            {
                ResetWindows();
                consoleOutputText.text = "All Programs Position Have Been Reset";
            }
            else
            {
                consoleOutputText.text = "Unknown command: " + userInput;
            }
        }

        consoleInputField.text = string.Empty;
    }

    void ResetWindows()
    {
        for (int i = 0; i < WindowsToReset.Length; i++)
        {
            WindowsToReset[i].localPosition = initialPositions[i];
            WindowsToReset[i].localRotation = initialRotations[i];
            WindowsToReset[i].localScale = initialScales[i];
        }
    }
}
