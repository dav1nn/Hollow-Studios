using UnityEngine;
using TMPro;

public class ShowPassword : MonoBehaviour
{
    public TMP_InputField passwordField; 
    private bool isPasswordVisible = false; 

    public GameObject eyeClosed; 
    public GameObject eyeOpen;   

    public void TogglePasswordVisibility()
    {
        isPasswordVisible = !isPasswordVisible;

        if (isPasswordVisible)
        {
            
            passwordField.contentType = TMP_InputField.ContentType.Standard;
            eyeClosed.SetActive(false);
            eyeOpen.SetActive(true);
        }
        else
        {
            
            passwordField.contentType = TMP_InputField.ContentType.Password;
            eyeClosed.SetActive(true);
            eyeOpen.SetActive(false);
        }

        
        passwordField.ForceLabelUpdate();
    }
}
