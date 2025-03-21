using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DesktopIcon : MonoBehaviour, IPointerClickHandler
{
    public float doubleClickTime = 0.3f;
    private float lastClickTime = 0f;

    [SerializeField] private GameObject filePanel; 
    [SerializeField] private GameObject voidPanel; 
    [SerializeField] private string consolePanelName = "ConsolePanel"; 
    [SerializeField] private ConversationManager conversationManager;

    private Vector3 originalScale;

    private void Awake()
    {
        if (filePanel != null)
        {
            originalScale = filePanel.transform.localScale;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        float timeSinceLastClick = Time.time - lastClickTime;
        if (timeSinceLastClick <= doubleClickTime)
        {
            TryOpenFilePanel();
            if (conversationManager != null)
            {
                conversationManager.StartConversation();
            }
        }
        lastClickTime = Time.time;
    }

    private void TryOpenFilePanel()
    {
        
        if (filePanel.name == consolePanelName && voidPanel != null && voidPanel.activeSelf)
        {
            Debug.Log("Console Panel cannot open until Void Panel is gone.");
            return;
        }

        OpenFilePanel();
    }

    private void OpenFilePanel()
    {
        if (filePanel != null)
        {
            filePanel.SetActive(true);
            filePanel.transform.localScale = Vector3.zero;
            StartCoroutine(AnimatePanel(filePanel, 0.2f, originalScale));
        }
        else
        {
            Debug.LogWarning("No file panel is assigned.");
        }
    }

    private IEnumerator AnimatePanel(GameObject panel, float duration, Vector3 targetScale)
    {
        float timeElapsed = 0f;
        Vector3 startScale = panel.transform.localScale;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            panel.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        panel.transform.localScale = targetScale;
    }

    public void CloseFilePanel()
    {
        if (filePanel != null && filePanel.activeSelf)
        {
            filePanel.SetActive(false);
        }
    }
}



