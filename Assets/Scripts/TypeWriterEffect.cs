using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypewriterEffect : MonoBehaviour
{
    [System.Serializable]
    public class Segment
    {
        public TextMeshProUGUI textComponent;
        [TextArea(3, 10)]
        public string fullText;
    }

    public Segment[] segments;
    public float typeSpeed = 0.05f;
    public float postSegmentDelay = 1f;

    void Start()
    {
        StartCoroutine(ShowSegments());
    }

    IEnumerator ShowSegments()
    {
        for (int s = 0; s < segments.Length; s++)
        {
            segments[s].textComponent.text = "";
            int i = 0;
            while (i < segments[s].fullText.Length)
            {
                if (segments[s].fullText[i] == '<')
                {
                    int closingIndex = segments[s].fullText.IndexOf('>', i);
                    if (closingIndex != -1)
                    {
                        segments[s].textComponent.text += segments[s].fullText.Substring(i, closingIndex - i + 1);
                        i = closingIndex + 1;
                        continue;
                    }
                }
                segments[s].textComponent.text += segments[s].fullText[i];
                i++;
                yield return new WaitForSeconds(typeSpeed);
            }
            yield return new WaitForSeconds(postSegmentDelay);
        }
        
        Debug.Log("Final text complete. Press Space to continue.");
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}












