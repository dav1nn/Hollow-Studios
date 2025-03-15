using UnityEngine;
using TMPro;

public class NotePanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField noteInputField;
    private const string NoteKey = "SavedNoteText";

    private void Start()
    {
        if (PlayerPrefs.HasKey(NoteKey))
        {
            noteInputField.text = PlayerPrefs.GetString(NoteKey);
        }

        noteInputField.onValueChanged.AddListener(OnTextChanged);
    }

    private void OnTextChanged(string newText)
    {
        PlayerPrefs.SetString(NoteKey, newText);
        PlayerPrefs.Save();
    }
}