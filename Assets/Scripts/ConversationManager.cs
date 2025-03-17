using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    public GameObject chatPanel;
    public TextMeshProUGUI npcText;
    public Button[] responseButtons;
    public DialogueNode[] nodes;
    private bool conversationStarted = false;
    private bool isTyping = false;
    private int currentNodeIndex = 0;
    private string conversationText = "";
    private string playerUsername;
    private Coroutine typingDotsCoroutine;

    private void Start()
    {
        playerUsername = PlayerPrefs.GetString("PlayerUsername", "User");
    }

    public void StartConversation()
    {
        Debug.Log("Conversation started.");
        if (!conversationStarted)
        {
            conversationStarted = true;
            currentNodeIndex = 0;
            conversationText = "<color=#8B0000>Void:</color> " + nodes[currentNodeIndex].npcDialogue;
            npcText.text = conversationText;
            UpdateUI();
        }
        chatPanel.SetActive(true);
        if (isTyping)
        {
            HideResponseButtons();
        }
        else
        {
            npcText.text = conversationText;
            UpdateUI();
        }
    }

    public void OnPlayerResponse(int responseIndex)
    {
        conversationText += "\n\n" + playerUsername + ": " + nodes[currentNodeIndex].playerResponses[responseIndex];
        npcText.text = conversationText;
        int nextIndex = nodes[currentNodeIndex].nextNodes[responseIndex];
        if (nextIndex < 0 || nextIndex >= nodes.Length)
        {
            EndConversation();
            return;
        }
        currentNodeIndex = nextIndex;
        isTyping = true;
        HideResponseButtons();
        StartCoroutine(ShowMomTypingRoutine());
    }

    private IEnumerator ShowMomTypingRoutine()
    {
        float waitBeforeTyping = Random.Range(2f, 5f);
        yield return new WaitForSeconds(waitBeforeTyping);
        string baseText = conversationText + "\n\n<color=#8B0000>Void: is typing</color>";
        typingDotsCoroutine = StartCoroutine(TypeDotsCoroutine(baseText));
        float typingDuration = Random.Range(4f, 7f);
        yield return new WaitForSeconds(typingDuration);
        if (typingDotsCoroutine != null)
        {
            StopCoroutine(typingDotsCoroutine);
        }
        conversationText += "\n\n<color=#8B0000>Void:</color> " + nodes[currentNodeIndex].npcDialogue;
        npcText.text = conversationText;
        isTyping = false;
        UpdateUI();
    }

    private IEnumerator TypeDotsCoroutine(string baseText)
    {
        int dotCount = 0;
        while (true)
        {
            dotCount = (dotCount % 3) + 1;
            npcText.text = baseText + new string('.', dotCount);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void UpdateUI()
    {
        string[] responses = nodes[currentNodeIndex].playerResponses;
        for (int i = 0; i < responseButtons.Length; i++)
        {
            if (i < responses.Length && !string.IsNullOrEmpty(responses[i]))
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

    private void HideResponseButtons()
    {
        for (int i = 0; i < responseButtons.Length; i++)
        {
            responseButtons[i].gameObject.SetActive(false);
        }
    }

    private void EndConversation()
    {
        chatPanel.SetActive(false);
    }
}


