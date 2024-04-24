using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    public MusicManager musicManager;

    void Start()
    {
        MusicManager.Instance.PlayMusic("MainMenuBG");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level01");
        MusicManager.Instance.PlayMusic("CaveMineBG");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}