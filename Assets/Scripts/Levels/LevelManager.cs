using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string[] levelNames; // ARRAY FOR LEVEL NAMES
    private int currentLevelIndex = 0;

    void Awake()
    {
        Debug.Log("LevelManager Loaded, Will not be Destroyed!");
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // DETERMINE CURRENT LEVEL INDEX BASED ON SCENE CURRENTLY LAODED
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
            currentLevelIndex++;
            SceneManager.LoadScene(levelNames[currentLevelIndex]);
        }
        else
        {
            Debug.Log("All levels completed!");
            // RESERVE LOGIC FOR WHEN ALL LEVELS ARE COMPLETED. (CONGRATULATION SPLASH SCREEN, CUTSCENE, ETC.)
        }
    }
}
