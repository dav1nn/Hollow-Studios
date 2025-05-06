using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    public GameObject panel; 

    
    public void OpenPanel()
    {
        panel.SetActive(true);
    }

    
    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
