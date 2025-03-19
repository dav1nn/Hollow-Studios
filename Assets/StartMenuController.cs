using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

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

    private bool isStartMenuOpen = false;

    void Start()
    {
        startMenuPanel.SetActive(false);
        shutdownConfirm.SetActive(false);
        shutdownCancel.SetActive(false);
        restartConfirm.SetActive(false);
        restartCancel.SetActive(false);
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
    }

    public void ShowRestartConfirmation()
    {
        restartButton.SetActive(false);
        restartConfirm.SetActive(true);
        restartCancel.SetActive(true);
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
}
