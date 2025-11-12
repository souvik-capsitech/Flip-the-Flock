using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Music")]
    public AudioSource bgMusicSource;
    public AudioClip bgMusicClip;

    [Header("SFX Source")]
    public AudioSource sfxSource;

    [Header("SFX Clips")]
    public AudioClip clickSFX;
    public AudioClip matchSFX;
    public AudioClip failSFX;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public AudioClip buttonClickSFX;

    [Header("Volume")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolumes();
    }

    private void Start()
    {
        PlayBackgroundMusic();
        FadeInMusic(0.5f);
    }



    public void PlayBackgroundMusic()
    {
        if (bgMusicSource == null || bgMusicClip == null) return;

        bgMusicSource.clip = bgMusicClip;
        bgMusicSource.loop = true;
        bgMusicSource.volume = musicVolume;

        if (!bgMusicSource.isPlaying)
            bgMusicSource.Play();
    }

    public void FadeInMusic(float duration)
    {
        StartCoroutine(FadeInCoroutine(duration));
    }

    IEnumerator FadeInCoroutine(float duration)
    {
        float startVol = 0f;
        float endVol = musicVolume;

        bgMusicSource.volume = 0f;
        bgMusicSource.Play();

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            bgMusicSource.volume = Mathf.Lerp(startVol, endVol, t / duration);
            yield return null;
        }
    }



    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        if (sfxSource == null) return;

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

 

  

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        bgMusicSource.volume = value;
        SaveVolumes();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        sfxSource.volume = value;
        SaveVolumes();
    }


    void SaveVolumes()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    void LoadVolumes()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }
}
