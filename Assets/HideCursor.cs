using UnityEngine;

public class HideCursorAndDisableClick : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Debug.Log("Left click is disabled in this scene.");
        }
    }

    void OnDisable()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
