using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    RectTransform rect;
    Vector3 origin;
    [SerializeField] public Vector3 direction;
    public Image JoyStickArea;
    public Image JoyStickHandle;
    private Vector3 turnedInputVector;

    //Movement
    public GameObject Player;
    public Camera mainCamera;
    [SerializeField] private float facing;

    float magnitude;
    public float sensitivity = 0.005f;

    //Rotation
    public Transform root;
    public float angles;
    public float RotateSpeed;

    public void Start()
    {
        rect = GetComponent<RectTransform>();
        origin = rect.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Movement
        rect.position = eventData.position;
        direction = rect.position - origin;
        direction.Normalize();

        //Try to lock joystick

        Vector2 Pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(JoyStickArea.rectTransform, eventData.position, eventData.pressEventCamera, out Pos))
        {
            Pos.x = (Pos.x / JoyStickArea.rectTransform.sizeDelta.x);
            Pos.y = (Pos.y / JoyStickArea.rectTransform.sizeDelta.y);

            Vector3 InputVector = new Vector3(Pos.x * 2 + 1, Pos.y * 2 - 1, 0);
            //InputVector = mainCamera.transform.TransformDirection(InputVector);
            turnedInputVector = Quaternion.Euler(0, 0, facing) * InputVector;
            turnedInputVector = (turnedInputVector.magnitude > 1.0f) ? turnedInputVector.normalized : turnedInputVector;

            JoyStickHandle.rectTransform.anchoredPosition = new Vector3(turnedInputVector.x * (JoyStickArea.rectTransform.sizeDelta.x / 2), (turnedInputVector.y * JoyStickArea.rectTransform.sizeDelta.y / 2));
        }

        //Rotation
        // angles = (Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
        angles = Mathf.Atan2(direction.x, direction.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rect.position = origin;
        direction = Vector3.zero;
    }

    void Update()
    {
        facing = mainCamera.transform.eulerAngles.y;
        //Calculate Sensitivity and Magnitude
        magnitude = Vector3.Distance(rect.position, origin);

        Vector3 velocity = Vector3.zero;
        velocity.x = direction.x * magnitude * sensitivity;
        velocity.z = direction.y * magnitude * sensitivity;

        Player.transform.Translate(velocity * Time.deltaTime, Space.World);
    }
}
