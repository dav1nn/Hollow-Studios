using System.Collections;
using UnityEngine;

public class AppPopup : MonoBehaviour
{
    public GameObject chatboxPanel;
    private ChatManager chatManager;

    private float clickTime = 0;
    private int clickCount = 0;
    private float doubleClickThreshold = 0.25f; 

    void Start()
    {
        chatManager = GetComponent<ChatManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            clickCount++;
            if (clickCount == 1)
            {
                clickTime = Time.time;
            }

            
            if (clickCount == 2 && (Time.time - clickTime) < doubleClickThreshold)
            {
                OpenPopup();
                clickCount = 0; 
            }
        }

        
        if ((Time.time - clickTime) > doubleClickThreshold)
        {
            clickCount = 0;
        }
    }

    public void OpenPopup()
    {
        chatboxPanel.SetActive(true);
        chatManager.DisplayMessage(chatManager.initialMessage); 
    }

    public void ClosePopup()
    {
        chatboxPanel.SetActive(false);
        chatManager.HideDialogueOptions();
    }
}
