using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupLoader : MonoBehaviour
{
    void Start()
    {
        
        if (PlayerPrefs.HasKey("LastScene"))
        {
            string lastScene = PlayerPrefs.GetString("LastScene");

            if (Application.CanStreamedLevelBeLoaded(lastScene))
            {
                SceneManager.LoadScene(lastScene);
                return;
            }
        }

        SceneManager.LoadScene("Scenes/Intro");
    }
}
