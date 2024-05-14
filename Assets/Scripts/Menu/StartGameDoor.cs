using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameDoor : InteractableObject
{
    public override void PerformAction()
    {
        // Load the first level
        SceneManager.LoadScene("Level01");
        Debug.Log("GAME START!");
    }
}
