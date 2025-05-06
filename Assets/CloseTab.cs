using UnityEngine;

public class CloseTab : MonoBehaviour
{
    public GameObject tab;

    public void Close()
    {
        tab.SetActive(false);
    }
}
