using UnityEngine;

public class QuitGameDoor : InteractableObject
{
    public override void PerformAction()
    {
        // Quit Game
        Application.Quit();
        Debug.Log("YOU'VE QUIT THE GAME, NOOO COMEBACK!");
    }
}
