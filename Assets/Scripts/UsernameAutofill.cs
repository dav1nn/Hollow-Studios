using UnityEngine;
using TMPro;
using System.Collections; // Add this namespace to use IEnumerator

public class UsernameAutofill : MonoBehaviour
{
    public TMP_InputField usernameField;

    void Start()
    {
        string systemUsername = System.Environment.UserName;

        usernameField.text = systemUsername;


    }

    IEnumerator AutoFillWithDelay(string systemUsername)
    {
        yield return new WaitForSeconds(2); 
        usernameField.text = systemUsername;
    }
}