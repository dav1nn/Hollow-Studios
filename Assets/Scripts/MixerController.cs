using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        float currentVolume;
        if (audioMixer.GetFloat("MasterVolume", out currentVolume))
        {
            volumeSlider.value = Mathf.Pow(10f, currentVolume / 20f); 
        }

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float sliderValue)
    {
        
        float curved = Mathf.Pow(sliderValue, 2f); 
        float dB = Mathf.Log10(Mathf.Clamp(curved, 0.0001f, 1f)) * 20f;

        audioMixer.SetFloat("MasterVolume", dB);
        Debug.Log($"Slider: {sliderValue:F4} | Curved: {curved:F4} | dB: {dB:F1}");
    }
}
