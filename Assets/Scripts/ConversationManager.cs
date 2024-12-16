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

    public void StartConversation()
    {
        currentNodeIndex = 0;
        UpdateUI();
        chatPanel.SetActive(true);
    }

    public void OnPlayerResponse(int responseIndex)
    {
        int nextIndex = nodes[currentNodeIndex].nextNodes[responseIndex];
        if (nextIndex < 0 || nextIndex >= nodes.Length)
        {
            EndConversation();
            return;
        }
        currentNodeIndex = nextIndex;
        UpdateUI();
    }

    private void UpdateUI()
    {
        npcText.text = nodes[currentNodeIndex].npcDialogue;
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

