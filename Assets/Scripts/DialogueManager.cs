using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Dialogue {
    public string question;
    public string[] answers;
}

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public Button[] answerButtons;
    public Dialogue[] dialogues;
    public float typeSpeed = 0.05f;

    private int dialogueIndex = 0;
    private Coroutine typingCoroutine;

    void Start() {
        DisplayDialogue(dialogues[dialogueIndex]);
    }

    public void OnAnswerClicked() {
        dialogueIndex++;
        if (dialogueIndex < dialogues.Length) {
            DisplayDialogue(dialogues[dialogueIndex]);
        } else {
            Debug.Log("Dialogue completed.");
        }
    }

    void DisplayDialogue(Dialogue dialogue) {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(dialogue.question));
        UpdateButtons(dialogue.answers);
    }

    IEnumerator TypeText(string message) {
        dialogueText.text = "";
        foreach (char letter in message.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    void UpdateButtons(string[] answers) {
        for (int i = 0; i < answerButtons.Length; i++) {
            if (i < answers.Length && !string.IsNullOrEmpty(answers[i])) {
                TMP_Text btnText = answerButtons[i].GetComponentInChildren<TMP_Text>();
                btnText.text = answers[i];
                answerButtons[i].gameObject.SetActive(true);
            } else {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }
}





