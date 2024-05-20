using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string[] levelNames; // ARRAY FOR LEVEL NAMES
    private int currentLevelIndex = 0;
    private PlayerController playerController;

    void Awake()
    {
        Debug.Log("LevelManager Loaded, Will not be Destroyed!");
        DontDestroyOnLoad(gameObject);

        // SUB TO SCENE
        SceneManager.sceneLoaded += OnSceneLoaded;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    void OnDestroy()
    {
        // UNSUBSCRIBE SCENE, TO AVOID MEMORY LEAKS
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // IF LOAD SCENE =/= MM, DETERMINE CURRENT LEVEL INDEX
        if (scene.name != "MainMenu")
        {
            DetermineCurrentLevelIndex();

            if (playerController != null) 
            { 
                playerController.ResetPlayerState();
            }
        }
    }

    private void DetermineCurrentLevelIndex()
    {
        // DETERMINE CURRENT LEVEL INDEX BASED ON SCENE CURRENTLY LOADED
        for (int i = 0; i < levelNames.Length; i++)
        {
            if (SceneManager.GetActiveScene().name == levelNames[i])
            {
                currentLevelIndex = i;
                break;
            }
        }
    }

    public void LoadNextLevel()
    {
        if (currentLevelIndex < levelNames.Length - 1)
        {
            // LOAD NEXT LEVEL
            currentLevelIndex++;
            SceneManager.LoadScene(levelNames[currentLevelIndex]);
        }
        else
        {
            Debug.Log("All levels completed! GRATS!!!");
            // RESERVE LOGIC FOR WHEN ALL LEVELS ARE COMPLETED. (CONGRATULATION SPLASH SCREEN, CUTSCENE, ETC.)
        }
    }
}