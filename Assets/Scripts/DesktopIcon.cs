using UnityEngine;
using UnityEngine.EventSystems;

public class DesktopIcon : MonoBehaviour, IPointerClickHandler
{
    public float doubleClickTime = 0.3f;
    private float lastClickTime = 0f;

    [SerializeField] private GameObject filePanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        float timeSinceLastClick = Time.time - lastClickTime;
        if (timeSinceLastClick <= doubleClickTime)
        {
            OpenFilePanel();
        }

        lastClickTime = Time.time;
    }

    private void OpenFilePanel()
    {
        if (filePanel != null)
        {
            filePanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No file.");
        }
    }
}