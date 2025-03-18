using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Dialogue {
    public string question;
    public string[] answers;
}

public class DialogueManager : MonoBehaviour {
    public TMP_Text dialogueText;
    public Button[] answerButtons;
    public Dialogue[] dialogues;
    public float typeSpeed = 0.05f;
    public Image shutdownEffectImage;
    public float fadeDuration = 2f;
    public bool doShutdown = true;
    public bool hidePanelAfterTyping = false;
    public GameObject dialoguePanel;
    
    private int dialogueIndex = 0;
    private Coroutine typingCoroutine;

    void Start() {
        if (dialogues.Length > 0) {
            DisplayDialogue(dialogues[dialogueIndex]);
        } else {
            Debug.LogError("No dialogues assigned.");
        }
        if (shutdownEffectImage != null) {
            Color c = shutdownEffectImage.color;
            c.a = 0f;
            shutdownEffectImage.color = c;
        }
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
        
        bool hidePanel = hidePanelAfterTyping && AllAnswersEmpty(dialogue.answers);
        if (AllAnswersEmpty(dialogue.answers)) {
            if (doShutdown)
                typingCoroutine = StartCoroutine(TypeTextAndShutdown(dialogue.question, hidePanel));
            else
                typingCoroutine = StartCoroutine(TypeText(dialogue.question, hidePanel));
        } else {
            typingCoroutine = StartCoroutine(TypeText(dialogue.question, false));
        }
        UpdateButtons(dialogue.answers);
    }

    IEnumerator TypeText(string message, bool hidePanel) {
        dialogueText.text = "";
        foreach (char letter in message.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        if (hidePanel && dialoguePanel != null) {
            yield return new WaitForSeconds(2f);
            dialoguePanel.SetActive(false);
        }
    }

    IEnumerator TypeTextAndShutdown(string message, bool hidePanel) {
        dialogueText.text = "";
        foreach (char letter in message.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        yield return new WaitForSeconds(2f);
        if (hidePanel && dialoguePanel != null)
            dialoguePanel.SetActive(false);
        if (doShutdown && shutdownEffectImage != null) {
            yield return StartCoroutine(ShutdownEffect());
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("Pro 2");
        }
    }

    void UpdateButtons(string[] answers) {
        for (int i = 0; i < answerButtons.Length; i++) {
            if (answers != null && i < answers.Length && !string.IsNullOrEmpty(answers[i])) {
                TMP_Text btnText = answerButtons[i].GetComponentInChildren<TMP_Text>();
                btnText.text = answers[i];
                answerButtons[i].gameObject.SetActive(true);
            } else {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    bool AllAnswersEmpty(string[] answers) {
        if (answers == null || answers.Length == 0)
            return true;
        foreach (var ans in answers) {
            if (!string.IsNullOrEmpty(ans))
                return false;
        }
        return true;
    }

    IEnumerator ShutdownEffect() {
        if (shutdownEffectImage == null) {
            Debug.LogWarning("Shutdown effect image not assigned.");
            yield break;
        }
        shutdownEffectImage.gameObject.SetActive(true);
        float timer = 0f;
        Color c = shutdownEffectImage.color;
        c.a = 0f;
        shutdownEffectImage.color = c;
        while (timer < fadeDuration) {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            c.a = Mathf.Lerp(0f, 1f, t);
            shutdownEffectImage.color = c;
            yield return null;
        }
        c.a = 1f;
        shutdownEffectImage.color = c;
    }
}

















