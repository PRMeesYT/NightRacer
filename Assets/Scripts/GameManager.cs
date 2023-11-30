using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject lapTimes;
    [SerializeField] private GameObject muteAudio;
    [SerializeField] private GameObject unMuteAudio;

    private AudioManager audioManager;

    public AudioClip buttonClickSFX;
    public AudioClip buttonHoverSFX;
    public AudioClip powerUpPickupSFX;
    public AudioClip rainSFX;
    public AudioClip winSoundSFX;
    public AudioClip medalSound;
    public AudioClip carSFX;
    public AudioClip carRepeatSFX;
    public AudioClip music1;

    private bool pauseOpen = false;
    private bool settingsOpen = false;
    private bool lapTimesOpen = false;
    private bool isPlaying = false;
    private bool audioMuted = false;


    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        AudioManager.Instance.PlaySFX(rainSFX, 0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
            ClickSound();
        }

        if (Input.GetKeyDown(KeyCode.W) && !isPlaying)
        {
            if (audioManager.carSfxSource != null && isPlaying)
            {
                audioManager.carSfxSource.Stop();
            }

            AudioManager.Instance.PlayCarSFX(carSFX, 1);
            isPlaying = true;
            StartCoroutine(RepeatSound());
        }

        if (Input.GetKeyUp(KeyCode.W) && isPlaying)
        {
            audioManager.StopCarSFX(carSFX);
            isPlaying = false;
        }
    }

    IEnumerator RepeatSound()
    {
        yield return new WaitForSeconds(carRepeatSFX.length);

        if (Input.GetKey(KeyCode.W) && isPlaying)
        {
            if (audioManager.carSfxSource != null && isPlaying)
            {
                audioManager.carSfxSource.Stop();
            }

            AudioManager.Instance.PlayCarSFX(carRepeatSFX, 1);
        }

        yield return new WaitForSeconds (carRepeatSFX.length);

        if (Input.GetKey(KeyCode.W) && isPlaying)
        {
            AudioManager.Instance.PlayCarSFX(carRepeatSFX, 1);
            StartCoroutine(RepeatSound());
        }
    }

    public void AudioEffects()
    {
        if (audioMuted)
        {
            muteAudio.SetActive(true);
            unMuteAudio.SetActive(false);
            audioMuted = false;

        }
        if (!audioMuted)
        {
            muteAudio.SetActive(false);
            unMuteAudio.SetActive(true);
            audioMuted = true;
        }
    }

    public void HoverSound()
    {
        AudioManager.Instance.PlaySFX(buttonHoverSFX, 1);
    }

    public void ClickSound()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX, 1);
    }

    public void PowerPickupSound()
    {
        AudioManager.Instance.PlaySFX(powerUpPickupSFX, 1);
    }

    public void FinishSound()
    {
        AudioManager.Instance.PlaySFX(winSoundSFX, 1);
    }

    public void MedalSound()
    {
        AudioManager.Instance.PlaySFX(medalSound, 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseMenu()
    {
        if (pauseOpen)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            pauseOpen = false;
        }
        else if (!pauseOpen)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            pauseOpen = true;
        }
    }

    public void SettingsMenu()
    {
        if (settingsOpen)
        {
            pauseMenu.SetActive(true);
            settingsMenu.SetActive(false);
            settingsOpen = false;
        }
        else if (!settingsOpen)
        {
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(true);
            settingsOpen = true;
        }
    }

        public void LapTImes()
    {
        if (lapTimesOpen)
        {
            lapTimes.SetActive(false);
            lapTimesOpen = false;
            pauseMenu.SetActive(true);
            pauseOpen = true;
        }
        else if (!lapTimesOpen)
        {
            lapTimes.SetActive(true);
            lapTimesOpen = true;
            pauseMenu.SetActive(false);
            pauseOpen = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
