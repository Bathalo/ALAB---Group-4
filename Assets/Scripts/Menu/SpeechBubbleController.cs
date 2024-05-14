using UnityEngine;
using TMPro;
using System.Collections;

public class SpeechBubbleController : MonoBehaviour
{
    public Transform masterNPC;
    public TextMeshPro speechText;
    private float timeSinceLastCall = 0f;
    private float delay = 4f; // Time
    public static string currentLevel = "Start"; // INITIAL LEVEL

    [SerializeField] private float floatAmplitude = 0.5f; // VALUE FOR HEIGHT
    [SerializeField] private float floatSpeed = 2.0f; // VALUE FOR ANIMATION SPEED

    private Vector3 initialPosition;

    public string[] StartDialogues = {
 
    };

    public string[] EndDialogues = {

    };

    private int currentPhraseIndex = 0;

    void Start()
    {
        initialPosition = transform.position;

        SelectRandomPhrase(currentLevel); // INITIAL SELECTION BASED ON CURRENT LEVEL

        InvokeRepeating("SelectRandomPhrase", 4f, 4f);
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        transform.position = new Vector3(initialPosition.x, initialPosition.y + yOffset, initialPosition.z);

        timeSinceLastCall += Time.deltaTime;

        if (timeSinceLastCall >= delay)
        {
            timeSinceLastCall = 0f; // RESETS THE TIMER
            SelectRandomPhrase(currentLevel); // CALLS SelectRandomPhrase TO CHANGE THE PHRASE
        }
    }
    void SelectRandomPhrase(string level)
    {
        string[] phrases = new string[0]; // INITIALIZE WITH AN EMPTY ARRAY

        if (level == "Start")
        {
            phrases = StartDialogues;
        }
        else if (level == "End")
        {
            phrases = EndDialogues;
        }

        // Safety Check
        if (phrases != null && phrases.Length > 0)
        {
            currentPhraseIndex = Random.Range(0, phrases.Length);  // RANDOM PHRASES 
            speechText.text = phrases[currentPhraseIndex]; 
        }
        else
        {
            Debug.LogWarning("Phrases array not assigned in SelectRandomPhrase");
        }
    }
}