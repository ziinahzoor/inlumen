using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool HealthbarAlwaysOn = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    private AudioSource AudioSource;
    [SerializeField] private AudioClip MenuHoverSound;
    [SerializeField] private AudioClip MenuClickSound;

    private AudioSource GameAudio;

    void Awake()
    {
        GameAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioSource.PlayOneShot(MenuClickSound);

            if (GameIsPaused)
            {
                if (pauseMenuUI.activeSelf)
                {
                    Resume();
                }
                else
                {
                    ReturnOptions();
                }
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        AudioSource.PlayOneShot(MenuClickSound);

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

        GameAudio.UnPause();
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        GameAudio.Pause();
        GameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        AudioSource.PlayOneShot(MenuClickSound);

        StartCoroutine(WaitForAudioToLoad(() =>
        {
            GameIsPaused = false;

            Time.timeScale = 1f;
            Loader.Load(Loader.Scene.MainMenuScene);
        }));
    }

    public void RestartGame()
    {
        AudioSource.PlayOneShot(MenuClickSound);

        StartCoroutine(WaitForAudioToLoad(() =>
        {
            GameIsPaused = false;

            Time.timeScale = 1f;
            GameAudio.Stop();
            Loader.Reload();
            GameAudio.Play();
        }));
    }

    public void QuitGame()
    {
        AudioSource.PlayOneShot(MenuClickSound);

        StartCoroutine(WaitForAudioToLoad(() =>
        {
            GameIsPaused = false;

            Application.Quit();
        }));
    }

    public void OpenOptions()
    {
        AudioSource.PlayOneShot(MenuClickSound);
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void ReturnOptions()
    {
        AudioSource.PlayOneShot(MenuClickSound);
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void ToggleHealthbars(Toggle toggle)
    {
        var alwaysOn = transform.Find("OptionsMenu/HealthbarAlwaysOnToggle").gameObject.GetComponent<Toggle>();
        alwaysOn.interactable = toggle.isOn;
        if (alwaysOn.isOn)
        {
            alwaysOn.isOn = false;
            ToggleAlwaysOnHealthbars(alwaysOn);
        }

        var healthbars = GameObject.FindGameObjectsWithTag("EnemyHealthbar");

        foreach (var healthbar in healthbars)
        {
            healthbar.transform.Find("HealthBar").gameObject.SetActive(toggle.isOn);
        }
    }

    public void ToggleAlwaysOnHealthbars(Toggle toggle)
    {
        HealthbarAlwaysOn = toggle.isOn;

        if (HealthbarAlwaysOn)
        {
            var healthbars = GameObject.FindGameObjectsWithTag("EnemyHealthbar");
            foreach (var healthbar in healthbars)
            {
                healthbar.transform.Find("HealthBar").gameObject.GetComponent<Canvas>().enabled = true;
            }
        }
    }

    private IEnumerator WaitForAudioToLoad(Action afterWait)
    {
        yield return new WaitWhile(() => AudioSource.isPlaying);

        afterWait?.Invoke();
    }

    public void triggerSoundOnHover()
    {
        AudioSource.PlayOneShot(MenuHoverSound);
    }
}


