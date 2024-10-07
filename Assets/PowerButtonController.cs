using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PowerButtonController : MonoBehaviour
{
    public Button powerButton;
    public VideoPlayer videoPlayer;
    public string nextSceneName = "login";

    private bool isVideoPlaying = false;

    void Start()
    {

        videoPlayer.gameObject.SetActive(false);

        powerButton.onClick.AddListener(OnPowerButtonClick);

    }

    void OnPowerButtonClick()
    {
        if (!isVideoPlaying)
        {
            powerButton.interactable = false;

            videoPlayer.gameObject.SetActive(true);
            videoPlayer.Play();

            isVideoPlaying = true;

            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        vp.loopPointReached -= OnVideoEnd;

        SceneManager.LoadScene(nextSceneName);
    }
}
