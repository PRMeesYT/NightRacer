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
    public AudioSource carRepeatSFX;
    public AudioClip music1;

    public float maxSpeed = 100f;
    public float acceleration = 5f;
    public float deceleration = 10f;

    private float currentSpeed = 0f;

    private bool pauseOpen = false;
    private bool settingsOpen = false;
    private bool lapTimesOpen = false;
    private bool isPlaying = false;
    private bool audioMuted = false;
    private bool gameStart = false;

    private float velocity;

    FlyingCarMovement[] cars;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        AudioManager.Instance.PlaySFX(rainSFX, 0.5f);

        cars = FindObjectsOfType<FlyingCarMovement>();

        foreach (FlyingCarMovement car in cars)
        {
            velocity = car.vertical;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
            ClickSound();
        }
        
        // Get input for acceleration and deceleration (you can modify this based on your input system)
        float accelerationInput = Input.GetAxis("Vertical") + Input.GetAxis("Vertical2");

        // Update car speed based on acceleration and deceleration
        if (accelerationInput > 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // Play engine sound based on current speed
        UpdateEngineSound();
    }



    private void UpdateEngineSound()
    {
        // Adjust pitch and volume of the engine sound based on the current speed
        float normalizedSpeed = currentSpeed / maxSpeed;
        carRepeatSFX.pitch = Mathf.Lerp(0.5f, 1.5f, normalizedSpeed);
        carRepeatSFX.volume = Mathf.Lerp(0.2f, 1.5f, normalizedSpeed);

        // You may want to add additional logic for handling other audio effects based on speed
    }

    //IEnumerator RepeatSound()
    //{
    //    if (decell)
    //    {
    //        Debug.Log("test");
    //        if (isPlaying)
    //        {
    //            yield return new WaitForSeconds(carDecellSFX.length);

    //            audioManager.StopCarSFX();
    //        }
    //        else
    //        {
    //            yield return new WaitForSeconds(carRepeatSFX.length);
    //        }

    //        isPlaying = false;
    //        canDecell = true;

    //        AudioManager.Instance.PlayCarSFX(carRepeatSFX, 1);

    //        yield return new WaitForSeconds(carRepeatSFX.length);

    //        if (isPlaying == false)
    //        {
    //            AudioManager.Instance.PlayCarSFX(carRepeatSFX, 1);
    //            StartCoroutine(RepeatSound());
    //        }
    //    }
    //    yield return null;
    //}

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
