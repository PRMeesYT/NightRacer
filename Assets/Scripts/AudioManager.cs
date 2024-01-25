using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource musicSource;
    private AudioSource sfxSource;
    private AudioSource carSfxSource;
    private float musicVolume = 1;

    [SerializeField] private AudioClip gameMusic;

    #region Instance
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned SoundManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        carSfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;

        PlayMusic(gameMusic);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayCarSFX(AudioClip clip, float volume)
    {
        carSfxSource.PlayOneShot(clip);
    }

    public void StopCarSFX()
    {
        carSfxSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = musicSource.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void MuteSFXVolume()
    {
        if (sfxSource.volume == 0)
        {
            sfxSource.volume = 1;
        }
        else if (sfxSource.volume == 1)
        {
            sfxSource.volume = 0;
        }
    }
}