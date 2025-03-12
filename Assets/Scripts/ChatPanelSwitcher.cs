using UnityEngine;
using UnityEngine.UI;

public class ChatPanelSwitcher : MonoBehaviour
{
    public GameObject momPanel;
    public GameObject dadPanel;
    public GameObject harryPanel;
    public GameObject anaPanel;

    public Button momButton;
    public Button dadButton;
    public Button harryButton;
    public Button anaButton;

    void Start()
    {
        momButton.onClick.AddListener(ShowMomPanel);
        dadButton.onClick.AddListener(ShowDadPanel);
        harryButton.onClick.AddListener(ShowHarryPanel);
        anaButton.onClick.AddListener(ShowAnaPanel);

        ShowMomPanel();
    }

    void ShowMomPanel()
    {
        momPanel.SetActive(true);
        dadPanel.SetActive(false);
        harryPanel.SetActive(false);
        anaPanel.SetActive(false);
    }

    void ShowDadPanel()
    {
        momPanel.SetActive(false);
        dadPanel.SetActive(true);
        harryPanel.SetActive(false);
        anaPanel.SetActive(false);
    }

    void ShowHarryPanel()
    {
        momPanel.SetActive(false);
        dadPanel.SetActive(false);
        harryPanel.SetActive(true);
        anaPanel.SetActive(false);
    }

    void ShowAnaPanel()
    {
        momPanel.SetActive(false);
        dadPanel.SetActive(false);
        harryPanel.SetActive(false);
        anaPanel.SetActive(true);
    }
}

