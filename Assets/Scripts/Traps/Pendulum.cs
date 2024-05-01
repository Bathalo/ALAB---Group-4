using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody2D rgb2d;

    public float moveSpeed;
    public float leftAngle;
    public float rightAngle;

    bool movingClockwise;
    // Start is called before the first frame update
    void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        movingClockwise = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void ChangeMoveDir()
    {
        if(transform.rotation.z > rightAngle)
        {
            movingClockwise = false;
        }
        else if (transform.rotation.z < leftAngle)
        {
            movingClockwise = true;
        }
    }

    public void Move()
    {
        ChangeMoveDir();
        if(movingClockwise)
        {
            rgb2d.angularVelocity = moveSpeed;
        }
        else
        {
            rgb2d.angularVelocity = -1*moveSpeed;
        }
    }
}
