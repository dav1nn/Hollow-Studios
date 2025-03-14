using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameClock : MonoBehaviour
{
    public TMP_Text clockText;
    private float gameTimeSeconds = 21f * 3600f;
    private const float TIME_SCALE = 340f;
    private bool transitionTriggered = false;
    private bool isTimeStopped = false;
    
    void Update()
    {
        if (!isTimeStopped)
            gameTimeSeconds += TIME_SCALE * Time.deltaTime;
            
        int totalSeconds = Mathf.FloorToInt(gameTimeSeconds);
        int hours24 = (totalSeconds / 3600) % 24;
        int minutes = (totalSeconds / 60) % 60;
        int hours12 = hours24 % 12;
        if (hours12 == 0)
            hours12 = 12;
        string ampm = hours24 < 12 ? "AM" : "PM";
        clockText.text = $"{hours12}:{minutes:00} {ampm}";
        
        if (hours24 >= 22 && !transitionTriggered)
        {
            transitionTriggered = true;
            isTimeStopped = true;
            gameTimeSeconds = 22f * 3600f;
            StartCoroutine(LoadNextScene());
        }
    }
    
    private IEnumerator LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.LogWarning("No next scene available in the build settings.");
        }
        yield break;
    }
}










