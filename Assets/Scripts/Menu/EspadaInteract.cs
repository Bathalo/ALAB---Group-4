using UnityEngine;

public class EspadaInteract : InteractableObject
{
    public EspadaDialogue dialogue;
    public override void PerformAction()
    {
        if (dialogue != null) 
        {
            dialogue.gameObject.SetActive(true);
        }
    }
}