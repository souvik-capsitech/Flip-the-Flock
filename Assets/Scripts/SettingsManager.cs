using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private float musicMax = 0.15f;   
    private float sfxMax = 1f;        

    void Start()
    {
        
        float savedMusic = PlayerPrefs.GetFloat("MusicSliderValue", 1f);
        float savedSFX = PlayerPrefs.GetFloat("SFXSliderValue", 1f);

        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;

    
        AudioManager.instance.SetMusicVolume(savedMusic * musicMax);
        AudioManager.instance.SetSFXVolume(savedSFX * sfxMax);

       
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);
    }

    void OnMusicSliderChanged(float sliderValue)
    {
        float volume = sliderValue * musicMax;
        AudioManager.instance.SetMusicVolume(volume);

        PlayerPrefs.SetFloat("MusicSliderValue", sliderValue);
    }

    void OnSFXSliderChanged(float sliderValue)
    {
        float volume = sliderValue * sfxMax;
        AudioManager.instance.SetSFXVolume(volume);

        PlayerPrefs.SetFloat("SFXSliderValue", sliderValue);
    }
}
