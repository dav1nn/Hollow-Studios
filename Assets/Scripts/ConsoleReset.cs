using UnityEngine;
using TMPro;

public class ConsoleReset : MonoBehaviour
{
    public TMP_InputField consoleInputField;
    public Transform[] WindowsToReset;

    private Vector3[] _initialPositions;
    private Quaternion[] _initialRotations;
    private Vector3[] _initialScales;

    void Start()
    {
        _initialPositions = new Vector3[WindowsToReset.Length];
        _initialRotations = new Quaternion[WindowsToReset.Length];
        _initialScales = new Vector3[WindowsToReset.Length];
        for (int i = 0; i < WindowsToReset.Length; i++)
        {
            _initialPositions[i] = WindowsToReset[i].localPosition;
            _initialRotations[i] = WindowsToReset[i].localRotation;
            _initialScales[i] = WindowsToReset[i].localScale;
        }
    }

    public void OnInputSubmit()
    {
        string userInput = consoleInputField.text;
        if (userInput.Equals("reset", System.StringComparison.OrdinalIgnoreCase))
        {
            ResetWindows();
        }
        consoleInputField.text = string.Empty;
    }

    private void ResetWindows()
    {
        for (int i = 0; i < WindowsToReset.Length; i++)
        {
            WindowsToReset[i].localPosition = _initialPositions[i];
            WindowsToReset[i].localRotation = _initialRotations[i];
            WindowsToReset[i].localScale = _initialScales[i];
        }
    }
}
