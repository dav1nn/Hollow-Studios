using UnityEngine;
using UnityEngine.UI;

public class BackgroundVolumeSlider : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider backgroundVolumeSlider;
    [SerializeField] private Slider secondaryVolumeSlider;
    [SerializeField] private Slider sceneMusicSlider;

    [Header("Secondary Audio Sources (Optional)")]
    [SerializeField] private AudioSource secondaryAudioSource1;
    [SerializeField] private AudioSource secondaryAudioSource2;
    [SerializeField] private AudioSource secondaryAudioSource3;

    private void Start()
    {
        
        float savedBackgroundVolume = PlayerPrefs.GetFloat("BackgroundVolume", 1f);
        backgroundVolumeSlider.value = savedBackgroundVolume;
        if (PersistentAudio.Instance != null)
            PersistentAudio.Instance.SetVolume(savedBackgroundVolume);

        
        float savedSecondaryVolume = PlayerPrefs.GetFloat("SecondaryVolume", 1f);
        secondaryVolumeSlider.value = savedSecondaryVolume;
        if (secondaryAudioSource1 != null)
            secondaryAudioSource1.volume = savedSecondaryVolume;
        if (secondaryAudioSource2 != null)
            secondaryAudioSource2.volume = savedSecondaryVolume;
        if (secondaryAudioSource3 != null)
            secondaryAudioSource3.volume = savedSecondaryVolume;

        
        sceneMusicSlider.value = PlayerPrefs.GetFloat("SceneMusicVolume", 1f);

        
        backgroundVolumeSlider.onValueChanged.AddListener(SetBackgroundVolume);
        secondaryVolumeSlider.onValueChanged.AddListener(SetSecondaryVolume);
        sceneMusicSlider.onValueChanged.AddListener(SetSceneMusicVolume);
    }

    public void SetBackgroundVolume(float value)
    {
        PlayerPrefs.SetFloat("BackgroundVolume", value);
        PlayerPrefs.Save();

        if (PersistentAudio.Instance != null)
            PersistentAudio.Instance.SetVolume(value);
    }

    public void SetSecondaryVolume(float value)
    {
        PlayerPrefs.SetFloat("SecondaryVolume", value);
        PlayerPrefs.Save();

        if (secondaryAudioSource1 != null)
            secondaryAudioSource1.volume = value;
        if (secondaryAudioSource2 != null)
            secondaryAudioSource2.volume = value;
        if (secondaryAudioSource3 != null)
            secondaryAudioSource3.volume = value;
    }

    public void SetSceneMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("SceneMusicVolume", value);
        PlayerPrefs.Save();
    }
}