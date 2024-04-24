using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider musicSlider;
    public Slider sfxSlider;
    public SoundManager soundManager;
    public MusicManager musicManager;

    private bool settingsChanged = false; // Flag to track if settings have been changed

    void Start()
    {
        // Load saved volume settings
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);
        settingsPanel.SetActive(false);
    }

    // ... (Your existing OpenSettings, CloseSettings, and SaveSettings functions) ...

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void SaveSettings()
    {
        if (settingsChanged) // Only save settings if they have been changed
        {
            PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
            PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
            PlayerPrefs.Save(); // Important!

            Debug.Log("Close Settings activated");
            CloseSettings();
            settingsChanged = false; // Reset flag after saving
        }
    }

    public void OnMusicSliderChanged()
    {
        float newMusicVolume = musicSlider.value;
        musicManager.SetMusicVolume(newMusicVolume);
        settingsChanged = true; // Set flag to indicate settings have changed
        Debug.Log("Music slider value: " + musicSlider.value);
    }

    public void OnSFXSliderChanged()
    {
        float newSFXVolume = sfxSlider.value;
        soundManager.SetSFXVolume(newSFXVolume);
        settingsChanged = true; // Set flag to indicate settings have changed
        Debug.Log("SFX slider value: " + sfxSlider.value);
    }
}