using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    public string playerOption1;
    public string playerOption2;
    public string playerOption3;

    public string npcResponseForOption1;
    public string npcResponseForOption2;
    public string npcResponseForOption3;

    
    public DialogueData nextDialogueForOption1;
    public DialogueData nextDialogueForOption2;
    public DialogueData nextDialogueForOption3;
}


