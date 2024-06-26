using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public PlayerController playerController;
    [SerializeField] private AudioSource hitAudioSource;

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.KBCounter = playerController.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerController.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerController.KnockFromRight = false;
            }

            // HIT AUDIO 
            if (hitAudioSource != null)
            {
                hitAudioSource.Play();
            }
        }
    }
}