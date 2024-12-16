using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string npcDialogue;
    public string[] playerResponses;
    public int[] nextNodes;
}
