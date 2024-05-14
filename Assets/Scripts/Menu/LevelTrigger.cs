using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public string levelName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpeechBubbleController.currentLevel = levelName;
        }
    }
}
