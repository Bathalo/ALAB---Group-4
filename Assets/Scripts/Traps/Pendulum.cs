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

    void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        movingClockwise = true;
    }

    void FixedUpdate()
    {
        MaintainSwing();
    }

    void ChangeMoveDir()
    {
        float zRotation = transform.eulerAngles.z;

        if (zRotation > 180) zRotation -= 360;

        if (zRotation > rightAngle)
        {
            movingClockwise = false;
        }
        else if (zRotation < leftAngle)
        {
            movingClockwise = true;
        }
    }

    void MaintainSwing()
    {
        ChangeMoveDir();
        float targetAngularVelocity = movingClockwise ? moveSpeed : -moveSpeed;
        rgb2d.angularVelocity = Mathf.Lerp(rgb2d.angularVelocity, targetAngularVelocity, Time.fixedDeltaTime * 5);
        float torque = movingClockwise ? moveSpeed * 0.1f : -moveSpeed * 0.1f;
        rgb2d.AddTorque(torque);
    }
}
