using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;

    public TMP_Text option1Text;
    public TMP_Text option2Text;
    public TMP_Text option3Text;

    public DialogueData currentDialogueData;

    private ChatManager chatManager;

    void Start()
    {
        chatManager = GetComponent<ChatManager>();

        
        option1Button.onClick.AddListener(() => OnOptionSelected(1));
        option2Button.onClick.AddListener(() => OnOptionSelected(2));
        option3Button.onClick.AddListener(() => OnOptionSelected(3));

        UpdateDialogueOptions(); 
    }

    private void UpdateDialogueOptions()
    {
        
        if (currentDialogueData != null)
        {
            option1Text.text = currentDialogueData.playerOption1;
            option2Text.text = currentDialogueData.playerOption2;
            option3Text.text = currentDialogueData.playerOption3;
        }
    }

    private void OnOptionSelected(int option)
    {
        
        string npcResponse = GetNPCResponse(option);
        chatManager.HideDialogueOptions(); 
        chatManager.DisplayMessage(npcResponse); 

        
        LoadNextDialogue(option);
    }

    private string GetNPCResponse(int option)
    {
        switch (option)
        {
            case 1: return currentDialogueData.npcResponseForOption1;
            case 2: return currentDialogueData.npcResponseForOption2;
            case 3: return currentDialogueData.npcResponseForOption3;
            default: return "";
        }
    }

    private void LoadNextDialogue(int option)
    {
        
        switch (option)
        {
            case 1:
                currentDialogueData = currentDialogueData.nextDialogueForOption1;
                break;
            case 2:
                currentDialogueData = currentDialogueData.nextDialogueForOption2;
                break;
            case 3:
                currentDialogueData = currentDialogueData.nextDialogueForOption3;
                break;
        }

        
        if (currentDialogueData != null)
        {
            UpdateDialogueOptions();
        }
    }
}
