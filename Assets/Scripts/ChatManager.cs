using System.Collections;
using UnityEngine;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public TMP_Text chatText;
    public GameObject dialogueOptionsPanel;
    public string initialMessage;

    private string currentNPCMessage;
    public float typingSpeed = 0.05f;

    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = GetComponent<DialogueManager>();
        dialogueOptionsPanel.SetActive(false);
    }

    public void DisplayMessage(string message)
    {
        currentNPCMessage = message;
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        chatText.text = "";
        foreach (char letter in currentNPCMessage)
        {
            chatText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        ShowDialogueOptions();
    }

    private void ShowDialogueOptions()
    {
        dialogueOptionsPanel.SetActive(true);
    }

    public void HideDialogueOptions()
    {
        dialogueOptionsPanel.SetActive(false);
    }
}

