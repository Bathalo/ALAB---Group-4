using System.Collections;
using UnityEngine;

public class JavelinTrap : MonoBehaviour
{
    [SerializeField] private GameObject javelinPrefab; 
    [SerializeField] private Transform spawnPoint; // SHOOT POINT
    [SerializeField] private float shootInterval = 4f; // SHOOT INTERVAL
    [SerializeField] private float javelinSpeed = 10f; // SPEED
    [SerializeField] private float javelinLifetime = 4f; // LIFETIME
    [SerializeField] private AudioSource shootAudioSource; // SHOOT AUDIO

    private AudioSource audioSource;
    private GameObject currentJavelin;

    void Start()
    {
        StartCoroutine(ShootJavelinRoutine());
    }

    private IEnumerator ShootJavelinRoutine()
    {
        while (true)
        {
            ShootJavelin();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    private void ShootJavelin()
    {
        // DESTROY JAVELIN
        if (currentJavelin != null)
        {
            Destroy(currentJavelin);
        }

        // NEW JAVELIN
        currentJavelin = Instantiate(javelinPrefab, spawnPoint.position, spawnPoint.rotation);

        // JAVELINE VELOCITY 
        Rigidbody2D rb = currentJavelin.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * javelinSpeed;

        // SHOOT AUDIO
        if (shootAudioSource != null)
        {
            shootAudioSource.Play();
        }

        // JAVELINE LIFETIME
        Destroy(currentJavelin, javelinLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.isAlive = false; // "KILLS" PLAYER
            }
        }
    }
}
