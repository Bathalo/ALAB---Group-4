using UnityEngine;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    public GatherInput gatherInput; // REFERENCE GATHERINPUT SCRIPT
    public TextMeshProUGUI interactText;

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange)
        {
            interactText.enabled = true; // SHOW HOVER UI
            Debug.Log("HoverUI Enabled");

            if (gatherInput.interactInput)
            {
                PerformAction();
            }
        }
        else
        {
            if (interactText.enabled)
            {
                interactText.enabled = false; // HIDE HOVER UI
                Debug.Log("HoverUI Disabled");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered range");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited range");
        }
    }

    public virtual void PerformAction()
    {
        // WILL BE OVERRIDDEN BY INTERACTABLEOBJECT SCRIPT METHOD 
    }
}