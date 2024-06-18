using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public float SavedMusicVolume = .5f;
    public float SavedSFXVolume = .5f;

    public bool init;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChargeMusicLevel();
    }
    public void ChargeMusicLevel()
    {

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                PlayMusic("MainMenu");
                init = true;
                break;
            case 1:

                switch (GameManager.Instance.difficulty)
                {
                    case GameManager.Difficulty.MEDIUM:
                        PlayMusic("Race");
                        break;
                    case GameManager.Difficulty.HARD:
                        PlayMusic("RaceHard");
                        break;
                    default:
                        PlayMusic("Race");
                        break;
                }

                init = true;
                break;
            case 2:

                if (GameManager.Instance.GetWin())
                {
                    PlayMusic("Victory");
                } else if (GameManager.Instance.GetLose())
                {
                    PlayMusic("Lose");
                }
                else
                {
                    PlayMusic("MainTheme");
                }

                init = true;
                break;
            case 3:
                PlayMusic("Credits");
                init = true;
                break;
            default:
                PlayMusic("MainTheme");
                init = true;
                break;
        }

        IncreaseVolume();
        musicSource.Play();
        //MusicVolume(SavedMusicVolume);
        SFXVolume(SavedSFXVolume);
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            musicSource.clip = s.clip;
        }
    }

    public void PlaySounds(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume * volume;
        SavedMusicVolume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume * volume;
        SavedSFXVolume = volume;
    }
    //Fade Volume


    public float fadeDuration = 8.0f; // Duración de la transición del volumen

    public void IncreaseVolume()
    {
        StartCoroutine(FadeVolume(0f, 0.5f, fadeDuration));
    }

    // Llamar a esta función para reducir el volumen del valor actual a 0
    public void DecreaseVolume()
    {
        StartCoroutine(FadeVolume(musicSource.volume, 0f, fadeDuration));
    }

    private IEnumerator FadeVolume(float startVolume, float targetVolume, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }
        musicSource.volume = targetVolume; // Asegurar que el volumen se ajuste exactamente al valor objetivo
    }


}

public class VolumeController : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al componente AudioSource
}