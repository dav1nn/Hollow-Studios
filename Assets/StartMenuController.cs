using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class StartMenuController : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject userProfileButton;
    public GameObject shutdownButton;
    public GameObject restartButton;
    public GameObject shutdownConfirm;
    public GameObject shutdownCancel;
    public GameObject restartConfirm;
    public GameObject restartCancel;
    public TMP_Text shutdownText;
    public TMP_Text restartText;

    private bool isStartMenuOpen = false;

    void Start()
    {
        ResetMenuState();
    }

    void Update()
    {
        if (isStartMenuOpen && Input.GetMouseButtonDown(0))
        {
            if (!IsClickInsideUI(startMenuPanel) && !IsClickInsideUI(userProfileButton))
            {
                ToggleStartMenu(false);
            }
        }
    }

    public void ToggleStartMenu(bool state)
    {
        isStartMenuOpen = state;
        startMenuPanel.SetActive(state);
        if (state)
        {
            ResetMenuState();
        }
    }

    public void OnUserProfileClick()
    {
        ToggleStartMenu(!isStartMenuOpen);
    }

    public void ShowShutdownConfirmation()
    {
        shutdownButton.SetActive(false);
        shutdownConfirm.SetActive(true);
        shutdownCancel.SetActive(true);
        shutdownText.gameObject.SetActive(false);
    }

    public void ConfirmShutdown()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void CancelShutdown()
    {
        shutdownButton.SetActive(true);
        shutdownConfirm.SetActive(false);
        shutdownCancel.SetActive(false);
        shutdownText.gameObject.SetActive(true);
    }

    public void ShowRestartConfirmation()
    {
        restartButton.SetActive(false);
        restartConfirm.SetActive(true);
        restartCancel.SetActive(true);
        restartText.gameObject.SetActive(false);
    }

    public void ConfirmRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CancelRestart()
    {
        restartButton.SetActive(true);
        restartConfirm.SetActive(false);
        restartCancel.SetActive(false);
        restartText.gameObject.SetActive(true);
    }

    private bool IsClickInsideUI(GameObject uiElement)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject == uiElement || result.gameObject.transform.IsChildOf(uiElement.transform))
                return true;
        }
        return false;
    }

    private void ResetMenuState()
    {
        shutdownButton.SetActive(true);
        restartButton.SetActive(true);
        shutdownConfirm.SetActive(false);
        shutdownCancel.SetActive(false);
        restartConfirm.SetActive(false);
        restartCancel.SetActive(false);
        shutdownText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
    }
}