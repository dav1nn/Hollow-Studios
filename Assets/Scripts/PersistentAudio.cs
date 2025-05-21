using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentAudio : MonoBehaviour
{
    private static PersistentAudio instance;
    private AudioSource audioSource;

    public string[] stopScenes;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void EnsureInstanceExists()
    {
        if (PlayerPrefs.HasKey("DisablePersistentAudio"))
            return;

        if (instance == null)
        {
            GameObject prefab = Resources.Load<GameObject>("PersistentAudio");
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab);
            }
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            if (audioSource != null && !audioSource.isPlaying)
                audioSource.Play();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (audioSource == null) return;

        bool shouldStop = false;

        foreach (string sceneName in stopScenes)
        {
            if (scene.name == sceneName)
            {
                shouldStop = true;
                break;
            }
        }

        if (shouldStop)
        {
            audioSource.Pause();
        }
        else
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    public static PersistentAudio Instance => instance;

    public void SetVolume(float volume)
    {
        if (audioSource != null)
            audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return audioSource != null ? audioSource.volume : 1f;
    }
}

