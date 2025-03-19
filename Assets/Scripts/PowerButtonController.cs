using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PowerButtonController : MonoBehaviour
{
    public Button powerButton;
    public string nextSceneName = "login";
    public GameObject flashScreen;
    private bool isFlashing = false;

    void Start()
    {
        flashScreen.SetActive(false);
        powerButton.onClick.AddListener(OnPowerButtonClick);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFlashing)
        {
            FlashScreen();
        }
    }

    void OnPowerButtonClick()
    {
        if (!isFlashing)
        {
            FlashScreen();
        }
    }

    void FlashScreen()
    {
        isFlashing = true;
        flashScreen.SetActive(true);
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
