using System.Collections;
using UnityEngine;

public class Level01_Goal : MonoBehaviour
{
    // Variables to control floating behavior
    [SerializeField] private float floatAmplitude = 0.5f; // Height
    [SerializeField] private float floatSpeed = 2.0f; // Speed of Animation

    private Vector3 initialPosition;

    void Start()
    {
        // Intitial Position
        initialPosition = transform.position;
    }

    void Update()
    {
        // Floating animation
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        transform.position = new Vector3(initialPosition.x, initialPosition.y + yOffset, initialPosition.z);

        // Spin Effect
        transform.Rotate(0f, Mathf.Sin(Time.time * floatSpeed * 0.5f), 0f);
    }
}
