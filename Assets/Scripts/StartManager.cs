using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainButtons;
    [SerializeField] private GameObject soloMenu;

    public AudioClip buttonClickSFX;
    public AudioClip music1;
    public AudioClip music2;

    private bool settingsOpen;
    private bool soloOpen;

    void Start()
    {
        AudioManager.Instance.PlayMusicWithCrossFade(music1, 3.0f);
        settingsMenu.SetActive(false);
        mainButtons.SetActive(true);
        soloMenu.SetActive(false);
        settingsOpen = false;
        soloOpen = false;
    }

    public void SettingsMenu()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX, 1);
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
        AudioManager.Instance.PlaySFX(buttonClickSFX, 1);

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

    public void LoadMap1()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX, 1);
        SceneManager.LoadScene("SoloMap1");
    }

    public void LoadMap2()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX, 1);
        SceneManager.LoadScene("SoloMap2");
    }

    public void LoadMap3()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX, 1);
        SceneManager.LoadScene("SoloMap3");
    }

    public void LoadMap4()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX, 1);
        SceneManager.LoadScene("SoloMap4");
    }

    public void VersusMode()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX, 1);
        SceneManager.LoadScene("VersusScene");
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX, 1);
        Application.Quit();
    }
}
