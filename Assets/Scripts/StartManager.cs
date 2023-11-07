using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainButtons;
    private bool settingsOpen;
    void Start()
    {
        settingsMenu.SetActive(false);
        mainButtons.SetActive(true);
        settingsOpen = false;
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
        SceneManager.LoadScene("SoloScene");
    }

    public void VersusMode()
    {
        SceneManager.LoadScene("VersusScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
