using System.Collections;
using System.Text;
using UnityEngine;
using TMPro;

public class CodeFlicker : MonoBehaviour
{
    public TextMeshProUGUI codeText;
    public float updateInterval = 2f;
    public int totalCharacters = 8000; 
    public string codeCharacters = "01<>!@#$%^&*(){}[];:'\",.\\|/?+-=";

    private void Start()
    {
        if (codeText == null)
            codeText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(UpdateCode());
    }

    private IEnumerator UpdateCode()
    {
        while (true)
        {
            string generatedCode = GenerateCode(totalCharacters);
            codeText.text = $"<color=#00FF00>{generatedCode}</color>";
            yield return new WaitForSeconds(updateInterval);
        }
    }

    private string GenerateCode(int length)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            int index = Random.Range(0, codeCharacters.Length);
            sb.Append(codeCharacters[index]);
        }
        return sb.ToString();
    }
}



