using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ThirdPersonCamera : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 25.0f;
    private const float Y_ANGLE_MAX = 60.0f;

    public VirtualJoystick joystick;

    public Transform camTransform { set; get; }
    private Transform thisTransform;
    private Camera cam;
    public Transform lookAt;

    public float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensitivityX = 2.5f;
    private float sensitivityY = 2.0f;

    void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        currentX += joystick.Horizontal() * sensitivityX;
        currentY += joystick.Vertical() * sensitivityY;

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
