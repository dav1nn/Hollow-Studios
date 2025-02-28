using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameClock : MonoBehaviour
{
    public TMP_Text clockText;
    public CanvasGroup dayPanel;
    public TMP_Text dayText;
    public string nextSceneName = "Desktop 2";
    private float gameTimeSeconds = 21f * 3600f;
    private const float TIME_SCALE = 40f;
    private bool transitionTriggered = false;
    private bool isTimeStopped = false;
    private int currentDay = 1;
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    void Update()
    {
        if(!isTimeStopped) gameTimeSeconds += TIME_SCALE * Time.deltaTime;
        int totalSeconds = Mathf.FloorToInt(gameTimeSeconds);
        int hours24 = (totalSeconds / 3600) % 24;
        int minutes = (totalSeconds / 60) % 60;
        int hours12 = hours24 % 12;
        if(hours12 == 0) hours12 = 12;
        string ampm = hours24 < 12 ? "AM" : "PM";
        clockText.text = $"{hours12}:{minutes:00} {ampm}";
        if(hours24 >= 22 && !transitionTriggered)
        {
            transitionTriggered = true;
            isTimeStopped = true;
            gameTimeSeconds = 22f * 3600f;
            StartCoroutine(DayTransition());
        }
    }

    private IEnumerator DayTransition()
    {
        dayPanel.gameObject.SetActive(true);
        yield return StartCoroutine(FadeCanvasGroup(dayPanel, 0f, 1f, fadeDuration));
        dayText.text = "Day " + currentDay;
        yield return new WaitForSeconds(displayDuration);
        currentDay++;
        dayText.text = "Day " + currentDay;
        yield return new WaitForSeconds(displayDuration);
        string oldSceneName = SceneManager.GetActiveScene().name;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
        while(!asyncLoad.isDone) yield return null;
        Scene newScene = SceneManager.GetSceneByName(nextSceneName);
        SceneManager.SetActiveScene(newScene);
        yield return StartCoroutine(FadeCanvasGroup(dayPanel, 1f, 0f, fadeDuration));
        SceneManager.UnloadSceneAsync(oldSceneName);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float t = 0f;
        while(t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, t / duration);
            yield return null;
        }
        cg.alpha = end;
    }
}







