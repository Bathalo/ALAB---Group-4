using System.Collections;
using UnityEngine;

public class JavelinTrap : MonoBehaviour
{
    [SerializeField] private GameObject javelinPrefab; // The javelin prefab to instantiate
    [SerializeField] private Transform spawnPoint; // The spawn point of the javelin
    [SerializeField] private float shootInterval = 4f; // Interval between each shot
    [SerializeField] private float javelinSpeed = 10f; // Speed of the javelin
    [SerializeField] private float javelinLifetime = 4f; // How long the javelin lasts before it disappears

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
        // Destroy the previous javelin if it exists
        if (currentJavelin != null)
        {
            Destroy(currentJavelin);
        }

        // Instantiate the new javelin
        currentJavelin = Instantiate(javelinPrefab, spawnPoint.position, spawnPoint.rotation);

        // Give the javelin a velocity to move it left
        Rigidbody2D rb = currentJavelin.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * javelinSpeed;

        // Destroy the javelin after its lifetime expires
        Destroy(currentJavelin, javelinLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.isAlive = false; // Set player's alive status to false
            }
        }
    }
}
