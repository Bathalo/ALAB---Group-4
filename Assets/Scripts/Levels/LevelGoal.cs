using System.Collections;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    // FLOAT BEHAVIOR
    [SerializeField] private float floatAmplitude = 0.5f; // VALUE FOR HEIGHT
    [SerializeField] private float floatSpeed = 2.0f; // VALUE FOR ANIMATION SPEED

    private Vector3 initialPosition;

    void Start()
    {
        // INITIAL POS
        initialPosition = transform.position;
    }

    void Update()
    {
        // FLOAT ANIMATION
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        transform.position = new Vector3(initialPosition.x, initialPosition.y + yOffset, initialPosition.z);

        // SPIN ANIMATION
        transform.Rotate(0f, Mathf.Sin(Time.time * floatSpeed * 0.5f), 0f);
    }
}