using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    public GameObject inGameMenuPanel;

    void Start()
    {
        inGameMenuPanel.SetActive(false);
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
        inGameMenuPanel.SetActive(!inGameMenuPanel.activeSelf); // TOGGLE
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Level00");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");

        Application.Quit();
    }
}
