using UnityEngine;
using UnityEngine.UI;

public class ChatPanelSwitcher : MonoBehaviour
{
    public GameObject momPanel;
    public GameObject dadPanel;

    public Button momButton;
    public Button dadButton;

    void Start()
    {
        momButton.onClick.AddListener(ActivateMomPanel);
        dadButton.onClick.AddListener(ActivateDadPanel);

        ActivateMomPanel();
    }

    void ActivateMomPanel()
    {
        momPanel.SetActive(true);
        dadPanel.SetActive(false);
    }

    void ActivateDadPanel()
    {
        dadPanel.SetActive(true);
        momPanel.SetActive(false);
    }
}
