using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    [Header("Main Chat Panel")]
    public GameObject chatPanel;
    public TextMeshProUGUI npcText;
    public Button[] responseButtons;

    [Header("Optional Additional Button to Disable")]
    public Button extraButton;

    [Header("Dialogue Nodes")]
    public DialogueNode[] nodes;

    [Header("Object to Fade In After Last 16 Nodes")]
    public GameObject objectToFadeIn;
    public List<GameObject> objectsToDeactivate;
    public float fadeDuration = 1f;

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

        if (currentNodeIndex >= nodes.Length - 16)
        {
            StartCoroutine(HideChatPanelAfterDelay(3f));
            StartCoroutine(TriggerFadeInAfterDelay(5f));
        }
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

    private IEnumerator HideChatPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (extraButton != null)
        {
            extraButton.gameObject.SetActive(false);
        }

        chatPanel.SetActive(false);
        conversationStarted = false;
    }

    private void EndConversation()
    {
        chatPanel.SetActive(false);
        if (extraButton != null)
        {
            extraButton.gameObject.SetActive(false);
        }
        conversationStarted = false;
    }

    private IEnumerator TriggerFadeInAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeInNow());
    }

    private IEnumerator FadeInNow()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
                yield return new WaitForSeconds(0.2f);
            }
        }
        if (objectToFadeIn != null)
        {
            objectToFadeIn.SetActive(true);
            CanvasGroup cg = objectToFadeIn.GetComponent<CanvasGroup>();
            if (cg == null) cg = objectToFadeIn.AddComponent<CanvasGroup>();
            cg.alpha = 0f;
            cg.interactable = false;
            cg.blocksRaycasts = false;
            float elapsed = 0f;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                cg.alpha = Mathf.Clamp01(elapsed / fadeDuration);
                yield return null;
            }
            cg.alpha = 1f;
            cg.interactable = true;
            cg.blocksRaycasts = true;
            objectToFadeIn.SetActive(true);
        }
    }
}
