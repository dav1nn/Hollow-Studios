using System.Collections;
using UnityEngine;
using TMPro;

public class ShutdownCountdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;

    private void Start()
    {
        StartCoroutine(StartShutdownSequence());
    }

    IEnumerator StartShutdownSequence()
    {
        yield return new WaitForSeconds(40f);

        countdownText.gameObject.SetActive(true);

        for (int i = 10; i > 0; i--)
        {
            countdownText.text = "SHUTTING DOWN - " + i;
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Game is closing...");
        Application.Quit();
    }
}
