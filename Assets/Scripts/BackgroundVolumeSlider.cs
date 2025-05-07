using UnityEngine;
using UnityEngine.UI;

public class BackgroundVolumeSlider : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider backgroundVolumeSlider;
    [SerializeField] private Slider secondaryVolumeSlider;
    [SerializeField] private Slider sceneMusicSlider;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource secondaryAudioSource1;
    [SerializeField] private AudioSource secondaryAudioSource2;
    [SerializeField] private AudioSource secondaryAudioSource3;
    [SerializeField] private AudioSource musicAudioSource;

    private void Awake()
    {
        
        float bgVolume = PlayerPrefs.HasKey("BackgroundVolume")
            ? PlayerPrefs.GetFloat("BackgroundVolume")
            : (PersistentAudio.Instance != null ? PersistentAudio.Instance.GetVolume() : 1f);

        backgroundVolumeSlider.SetValueWithoutNotify(bgVolume);
        if (PersistentAudio.Instance != null)
            PersistentAudio.Instance.SetVolume(bgVolume);

        
        float sfxDefault = secondaryAudioSource1 != null ? secondaryAudioSource1.volume : 1f;
        float sfxVolume = PlayerPrefs.HasKey("SecondaryVolume")
            ? PlayerPrefs.GetFloat("SecondaryVolume")
            : sfxDefault;

        if (secondaryAudioSource1 != null) secondaryAudioSource1.volume = sfxVolume;
        if (secondaryAudioSource2 != null) secondaryAudioSource2.volume = sfxVolume;
        if (secondaryAudioSource3 != null) secondaryAudioSource3.volume = sfxVolume;
        secondaryVolumeSlider.SetValueWithoutNotify(sfxVolume);

        
        
        float musicDefault = musicAudioSource != null ? musicAudioSource.volume : 1f;
        float musicVolume = PlayerPrefs.HasKey("SceneMusicVolume")
            ? PlayerPrefs.GetFloat("SceneMusicVolume")
            : musicDefault;

        if (musicAudioSource != null)
        {
            musicAudioSource.volume = musicVolume;

            if (!musicAudioSource.isPlaying)
                musicAudioSource.Play();
        }
        sceneMusicSlider.SetValueWithoutNotify(musicVolume);
    }

    private void Start()
    {
        backgroundVolumeSlider.onValueChanged.AddListener(SetBackgroundVolume);
        secondaryVolumeSlider.onValueChanged.AddListener(SetSecondaryVolume);
        sceneMusicSlider.onValueChanged.AddListener(SetSceneMusicVolume);
    }

    public void SetBackgroundVolume(float value)
    {
        if (PersistentAudio.Instance != null)
            PersistentAudio.Instance.SetVolume(value);

        PlayerPrefs.SetFloat("BackgroundVolume", value);
        PlayerPrefs.Save();
    }

    public void SetSecondaryVolume(float value)
    {
        if (secondaryAudioSource1 != null) secondaryAudioSource1.volume = value;
        if (secondaryAudioSource2 != null) secondaryAudioSource2.volume = value;
        if (secondaryAudioSource3 != null) secondaryAudioSource3.volume = value;

        PlayerPrefs.SetFloat("SecondaryVolume", value);
        PlayerPrefs.Save();
    }

    public void SetSceneMusicVolume(float value)
    {
        if (musicAudioSource != null)
            musicAudioSource.volume = value;

        PlayerPrefs.SetFloat("SceneMusicVolume", value);
        PlayerPrefs.Save();
    }
}


