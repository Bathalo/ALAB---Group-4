using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    public GameObject inGameMenuPanel;

    void Start()
    {
        inGameMenuPanel.SetActive(false); // Ensure menu is hidden at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        inGameMenuPanel.SetActive(!inGameMenuPanel.activeSelf); // Toggle visibility
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenuScene" with your actual scene name
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
