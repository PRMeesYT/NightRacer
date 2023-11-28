using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    public AudioClip buttonClickSFX;
    public AudioClip buttonHoverSFX;
    public AudioClip powerUpPickupSFX;
    public AudioClip rainSFX;
    public AudioClip winSoundSFX;
    public AudioClip medalSound;
    public AudioClip carSFX;
    public AudioClip music1;
    public AudioClip music2;

    private bool pauseOpen = false;
    private bool settingsOpen = false;

    private void Start()
    {
        AudioManager.Instance.PlaySFX(rainSFX, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
            ClickSound();
        }
    }

    public void HoverSound()
    {
        AudioManager.Instance.PlaySFX(buttonHoverSFX, 1);
    }

    public void CarSound()
    {
        AudioManager.Instance.PlaySFX(carSFX, 1);
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
            pauseMenu.SetActive(false);
            pauseOpen = false;
        }
        else if (!pauseOpen)
        {
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
