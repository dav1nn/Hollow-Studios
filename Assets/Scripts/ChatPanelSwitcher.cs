using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatPanelSwitcher : MonoBehaviour
{
    public GameObject momPanel;
    public GameObject dadPanel;
    public GameObject harryPanel;
    public GameObject anaPanel;
    public GameObject voidPanel;

    public Button momButton;
    public Button dadButton;
    public Button harryButton;
    public Button anaButton;
    public Button voidButton;

    bool voidActivated;

    void Awake()
    {
        if (voidButton) voidButton.gameObject.SetActive(false);
        if (voidPanel) voidPanel.SetActive(false);
    }

    void OnEnable()
    {
        ShowMomPanel();

        
        if (!voidActivated) StartCoroutine(WaitAndShowVoid(1f));
    }

    IEnumerator WaitAndShowVoid(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (voidButton) voidButton.gameObject.SetActive(true);
        voidActivated = true;
    }

    void Start()
    {
        if (momButton) momButton.onClick.AddListener(ShowMomPanel);
        if (dadButton) dadButton.onClick.AddListener(ShowDadPanel);
        if (harryButton) harryButton.onClick.AddListener(ShowHarryPanel);
        if (anaButton) anaButton.onClick.AddListener(ShowAnaPanel);
        if (voidButton) voidButton.onClick.AddListener(ShowVoidPanel);
    }

    void ShowMomPanel()
    {
        if (momPanel)   momPanel.SetActive(true);
        if (dadPanel)   dadPanel.SetActive(false);
        if (harryPanel) harryPanel.SetActive(false);
        if (anaPanel)   anaPanel.SetActive(false);
        if (voidPanel)  voidPanel.SetActive(false);
    }

    void ShowDadPanel()
    {
        if (momPanel)   momPanel.SetActive(false);
        if (dadPanel)   dadPanel.SetActive(true);
        if (harryPanel) harryPanel.SetActive(false);
        if (anaPanel)   anaPanel.SetActive(false);
        if (voidPanel)  voidPanel.SetActive(false);
    }

    void ShowHarryPanel()
    {
        if (momPanel)   momPanel.SetActive(false);
        if (dadPanel)   dadPanel.SetActive(false);
        if (harryPanel) harryPanel.SetActive(true);
        if (anaPanel)   anaPanel.SetActive(false);
        if (voidPanel)  voidPanel.SetActive(false);
    }

    void ShowAnaPanel()
    {
        if (momPanel)   momPanel.SetActive(false);
        if (dadPanel)   dadPanel.SetActive(false);
        if (harryPanel) harryPanel.SetActive(false);
        if (anaPanel)   anaPanel.SetActive(true);
        if (voidPanel)  voidPanel.SetActive(false);
    }

    void ShowVoidPanel()
    {
        if (momPanel)   momPanel.SetActive(false);
        if (dadPanel)   dadPanel.SetActive(false);
        if (harryPanel) harryPanel.SetActive(false);
        if (anaPanel)   anaPanel.SetActive(false);
        if (voidPanel)  voidPanel.SetActive(true);
    }
}






