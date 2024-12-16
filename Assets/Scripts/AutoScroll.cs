using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AutoScrollTMP : MonoBehaviour
{
    public ScrollRect scrollRect;
    public TMP_InputField inputField;

    void Start()
    {
        inputField.onValueChanged.AddListener((value) => ScrollToBottom());
    }

    void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
