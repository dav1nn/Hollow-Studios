using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string fullText;
    public float typeSpeed = 0.05f;

    void Start()
    {
        textComponent.text = "";
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        int i = 0;
        while (i < fullText.Length)
        {
            
            if (fullText[i] == '<')
            {
                int closingIndex = fullText.IndexOf('>', i);
                if (closingIndex != -1)
                {
                    
                    textComponent.text += fullText.Substring(i, closingIndex - i + 1);
                    i = closingIndex + 1;
                    continue;
                }
            }
            
            textComponent.text += fullText[i];
            i++;
            yield return new WaitForSeconds(typeSpeed);
        }
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}






