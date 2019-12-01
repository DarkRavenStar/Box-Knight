using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    public float Drag = 0.5f;
    public float terminalRotationSpeed = 25.0f;
    public Vector3 MoveVector { set; get; }
    public VirtualJoystick joystick;

    private Rigidbody thisRigidbody;

    private void Start()
    {
        thisRigidbody = gameObject.GetComponent<Rigidbody>();
        terminalRotationSpeed = thisRigidbody.maxAngularVelocity;
        Drag = thisRigidbody.drag;
    }

    private void Update()
    {
        MoveVector = PoolInput();
        Move();
    }

    private void Move()
    {
        thisRigidbody.AddForce(MoveVector * MoveSpeed);
    }

    private Vector3 PoolInput()
    {
        Vector3 dir = Vector3.zero;

        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();

        if (dir.magnitude > 1)
        {
            dir.Normalize();
        }

        return dir;
    }
}
