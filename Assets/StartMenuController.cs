using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class StartMenuController : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject userProfileButton;

    private bool isStartMenuOpen = false;

    void Start()
    {
        startMenuPanel.SetActive(false);
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

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
