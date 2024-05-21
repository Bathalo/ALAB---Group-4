using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private MusicLibrary musicLibrary;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private string mainMenuSceneName = "Level00"; // MAIN MENU SCENE 
    [SerializeField] private string gameplayTrackName = "GameplayMusic"; // BGM FOR LEVELS
    [SerializeField][Range(0, 1)] private float defaultVolume = 1f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // INITIAL VOLUME VALUE
        musicSource.volume = defaultVolume;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == mainMenuSceneName)
        {
            // STOP LEVEL BGM WHEN AT MAIN MENU
            StopMusic(0.5f); // SOME AUDIO FADE
        }
        else
        {
            // PLAY LEVEL BGM WHEN ENTERING LEVEL01
            PlayMusic(gameplayTrackName, 0.5f); // SOME AUDIOFADE
        }
    }

    public void PlayMusic(string trackName, float fadeDuration = 0.5f)
    {
        StartCoroutine(AnimateMusicCrossfade(musicLibrary.GetClipFromName(trackName), fadeDuration));
    }

    public void StopMusic(float fadeDuration = 0.5f)
    {
        StartCoroutine(AnimateMusicFadeOut(fadeDuration));
    }

    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        float percent = 0;
        float startVolume = musicSource.volume;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(startVolume, 0, percent);
            yield return null;
        }

        musicSource.clip = nextTrack;
        musicSource.Play();

        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, defaultVolume, percent);
            yield return null;
        }
    }

    IEnumerator AnimateMusicFadeOut(float fadeDuration = 0.5f)
    {
        float percent = 0;
        float startVolume = musicSource.volume;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(startVolume, 0, percent);
            yield return null;
        }

        musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
