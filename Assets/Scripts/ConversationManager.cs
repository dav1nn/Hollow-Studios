using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    public GameObject chatPanel;
    public TextMeshProUGUI npcText;
    public Button[] responseButtons;
    public DialogueNode[] nodes;

    private int currentNodeIndex = 0;
    private string conversationText = "";
    private string playerUsername;

    private void Start()
    {
        playerUsername = PlayerPrefs.GetString("PlayerUsername", "User");
    }

    public void StartConversation()
    {
        currentNodeIndex = 0;
        conversationText = "Mom: " + nodes[currentNodeIndex].npcDialogue;
        npcText.text = conversationText;
        UpdateUI();
        chatPanel.SetActive(true);
    }

    public void OnPlayerResponse(int responseIndex)
    {
        conversationText += "\n\n" + playerUsername + ": " + nodes[currentNodeIndex].playerResponses[responseIndex];
        int nextIndex = nodes[currentNodeIndex].nextNodes[responseIndex];
        if (nextIndex < 0 || nextIndex >= nodes.Length)
        {
            EndConversation();
            return;
        }
        currentNodeIndex = nextIndex;
        conversationText += "\n\nMom: " + nodes[currentNodeIndex].npcDialogue;
        npcText.text = conversationText;
        UpdateUI();
    }

    private void UpdateUI()
    {
        string[] responses = nodes[currentNodeIndex].playerResponses;
        for (int i = 0; i < responseButtons.Length; i++)
        {
            if (i < responses.Length)
            {
                responseButtons[i].gameObject.SetActive(true);
                responseButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = responses[i];
            }
            else
            {
                responseButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void EndConversation()
    {
        chatPanel.SetActive(false);
    }
}
