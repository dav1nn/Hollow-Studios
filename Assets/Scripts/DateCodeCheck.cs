using TMPro;
using UnityEngine;

public class DateCodeCheck : MonoBehaviour
{
    public TMP_InputField dayInput;
    public TMP_InputField monthInput;
    public TMP_InputField yearInput;
    public TMP_Text accessDeniedText;
    public GameObject currentGameObject;
    public GameObject nextGameObject;

    public void OnNextPressed()
    {
        if (dayInput.text == "27" && monthInput.text == "08" && yearInput.text == "1985")
        {
            currentGameObject.SetActive(false);
            nextGameObject.SetActive(true);
        }
        else
        {
            accessDeniedText.gameObject.SetActive(true);
        }
    }
}
