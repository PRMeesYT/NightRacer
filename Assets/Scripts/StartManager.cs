using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainButtons;
    [SerializeField] private GameObject soloMenu;
    [SerializeField] private GameObject creditsMenu;

    public AudioClip buttonClickSFX;
    public AudioClip buttonHoverSFX;

    private bool settingsOpen;
    private bool soloOpen;
    private bool creditsOpen;

    void Start()
    {
        settingsMenu.SetActive(false);
        mainButtons.SetActive(true);
        soloMenu.SetActive(false);
        creditsMenu.SetActive(false);
        settingsOpen = false;
        soloOpen = false;
        creditsOpen = false;
    }

    public void SettingsMenu()
    {
        if (settingsOpen)
        {
            mainButtons.SetActive(true);
            settingsMenu.SetActive(false);
            settingsOpen = false;
        }
        else if (!settingsOpen)
        {
            mainButtons.SetActive(false);
            settingsMenu.SetActive(true);
            settingsOpen = true;
        }
    }

    public void SoloMode()
    {
        if (soloOpen)
        {
            mainButtons.SetActive(true);
            soloMenu.SetActive(false);
            soloOpen = false;
        }
        else if (!soloOpen)
        {
            mainButtons.SetActive(false);
            soloMenu.SetActive(true);
            soloOpen = true;
        }
    }

    public void Credits()
    {
        if (creditsOpen)
        {
            mainButtons.SetActive(true);
            creditsMenu.SetActive(false);
            creditsOpen = false;
        }
        else if (!creditsOpen)
        {
            mainButtons.SetActive(false);
            creditsMenu.SetActive(true);
            creditsOpen = true;
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

    public void LoadMap1()
    {
        SceneManager.LoadScene("Track1");
    }

    public void LoadMap2()
    {
        SceneManager.LoadScene("Track2");
    }

    public void LoadMap3()
    {
        SceneManager.LoadScene("Track3");
    }

    public void LoadMap4()
    {
        SceneManager.LoadScene("Track4");
    }

    public void VersusMode()
    {
        SceneManager.LoadScene("Map1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
