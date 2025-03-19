using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameClock : MonoBehaviour
{
    public TMP_Text clockText;
    private float gameTimeSeconds;
    private float timer = 0f;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Day 1")
            gameTimeSeconds = (21f * 3600f) + (32f * 60f);
        else if (sceneName == "Day 2")
            gameTimeSeconds = (13f * 3600f) + (16f * 60f);
        else if (sceneName == "Day 3")
            gameTimeSeconds = (19f * 3600f) + (52f * 60f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10f)
        {
            timer = 0f;
            gameTimeSeconds += 60f;
        }

        int totalSeconds = Mathf.FloorToInt(gameTimeSeconds);
        int hours24 = (totalSeconds / 3600) % 24;
        int minutes = (totalSeconds / 60) % 60;
        int hours12 = hours24 % 12;
        if (hours12 == 0)
            hours12 = 12;
        string ampm = hours24 < 12 ? "AM" : "PM";
        clockText.text = $"{hours12}:{minutes:00} {ampm}";
    }
}
