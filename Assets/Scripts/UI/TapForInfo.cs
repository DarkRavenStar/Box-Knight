using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TapForInfo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject infoPanel;
    public Text CardName;
    public Text StaminaValue;
    public Text AttackValue;
    public Text DefenceValue;
    public Text HealValue;
    public Text DescriptionName;
    public bool InfoOn = false;

    private GameObject Card;
    private CardDisplay CardDisplay;
    private CardData CardData;
    private ConditionCardData ConCardData;

    private Vector3 Large = new Vector3(2.0f, 2.0f);
    private Vector3 Small = new Vector3(1.0f, 1.0f);

    public Transform InfoCard;
    public Transform InfoCardOrgPos;
    [SerializeField] private float FracJourney;
    public float Speed;

    public void Start()
    {
        infoPanel.SetActive(false);
        Card = this.gameObject;
        CardDisplay = Card.GetComponent<CardDisplay>();

		if (CardDisplay != null)
		{
			if(CardDisplay.card.IsConditionCard == false)
			{
				CardData = (CardData)CardDisplay.card;
			}
			else if(CardDisplay.card.IsConditionCard == true)
			{
				ConCardData = (ConditionCardData)CardDisplay.card;
			}

			InfoCard.gameObject.SetActive(false);
		}
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InfoOn = true;
        InfoCardSetup();
        SetupInfo();
        //ShowCard();
        infoPanel.SetActive(true);
        InfoCard.gameObject.SetActive(true);
    }

	public void OnPointerUp(PointerEventData eventData)
    {
        InfoOn = false;
        infoPanel.SetActive(false);
        InfoCard.gameObject.SetActive(false);
        //ReturnCard();
    }

    public void SetupInfo()
    {
		if(CardDisplay.card.IsConditionCard == false)
		{
			CardData = (CardData)CardDisplay.card;
		}
		else if(CardDisplay.card.IsConditionCard == true)
		{
			ConCardData = (ConditionCardData)CardDisplay.card;
		}
        if (CardDisplay.card.IsConditionCard == false)
        {
            CardName.text = CardDisplay.card.name;
            StaminaValue.text = CardDisplay.card.staminaCost.ToString();
            DescriptionName.text = CardData.fullDescription;
        }
        else if (CardDisplay.card.IsConditionCard == true)
        {
            CardName.text = CardDisplay.card.name;
            StaminaValue.text = CardDisplay.card.staminaCost.ToString();
            DescriptionName.text = ConCardData.description;
        }
    }

    public void ShowCard()
    {
        Card.transform.localScale = Large;
    }

    public void ReturnCard()
    {
        Card.transform.localScale = Small;
    }

	public void InfoCardSetup()
	{
		FracJourney = 0.0f;
		//InfoCard.position = Card.transform.position;
		//InfoCard.localScale = Card.transform.localScale;
		InfoCard.GetComponent<CardDisplay>().card = CardDisplay.card;
		InfoCard.GetComponent<CardDisplay>().Restart();
	}

    public void Update()
    {
        if (InfoOn == true)
        {
            FracJourney += Speed;
            InfoCard.position = Vector3.Lerp(InfoCard.position, InfoCardOrgPos.position, FracJourney);
            InfoCard.localScale = Vector3.Lerp(InfoCard.localScale, InfoCardOrgPos.localScale, FracJourney);
        }
    }
}

/*


You don't use the Input API for the new UI. You subscribe to UI events or implement interface depending on the event.

These are the proper ways to detect events on the new UI components:

1.Image, RawImage and Text Components:

Implement the needed interface and override its function. The example below implements the most used events.

using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Ended");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Mouse Up");
    }
}

2.Button Component:

You use events to register to Button clicks:

public class ButtonClickDetector : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    void OnEnable()
    {
        //Register Button Events
        button1.onClick.AddListener(() => buttonCallBack(button1));
        button2.onClick.AddListener(() => buttonCallBack(button2));
        button3.onClick.AddListener(() => buttonCallBack(button3));

    }

    private void buttonCallBack(Button buttonPressed)
    {
        if (buttonPressed == button1)
        {
            //Your code for button 1
            Debug.Log("Clicked: " + button1.name);
        }

        if (buttonPressed == button2)
        {
            //Your code for button 2
            Debug.Log("Clicked: " + button2.name);
        }

        if (buttonPressed == button3)
        {
            //Your code for button 3
            Debug.Log("Clicked: " + button3.name);
        }
    }

    void OnDisable()
    {
        //Un-Register Button Events
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
    }
}

If you are detecting something other than Button Click on the Button then use method 1. For example, Button down and not Button Click, use IPointerDownHandler and its OnPointerDown function from method 1.

3.InputField Component:

You use events to register to register for InputField submit:

public InputField inputField;

void OnEnable()
{
    //Register InputField Events
    inputField.onEndEdit.AddListener(delegate { inputEndEdit(); });
    inputField.onValueChanged.AddListener(delegate { inputValueChanged(); });
}

//Called when Input is submitted
private void inputEndEdit()
{
    Debug.Log("Input Submitted");
}

//Called when Input changes
private void inputValueChanged()
{
    Debug.Log("Input Changed");
}

void OnDisable()
{
    //Un-Register InputField Events
    inputField.onEndEdit.RemoveAllListeners();
    inputField.onValueChanged.RemoveAllListeners();
}

4.Slider Component:

To detect when slider value changes during drag:

public Slider slider;

void OnEnable()
{
    //Subscribe to the Slider Click event
    slider.onValueChanged.AddListener(delegate { sliderCallBack(slider.value); });
}

//Will be called when Slider changes
void sliderCallBack(float value)
{
    Debug.Log("Slider Changed: " + value);
}

void OnDisable()
{
    //Un-Subscribe To Slider Event
    slider.onValueChanged.RemoveListener(delegate { sliderCallBack(slider.value); });
}

For other events, use Method 1.

5.Dropdown Component

public Dropdown dropdown;
void OnEnable()
{
    //Register to onValueChanged Events

    //Callback with parameter
    dropdown.onValueChanged.AddListener(delegate { callBack(); });

    //Callback without parameter
    dropdown.onValueChanged.AddListener(callBackWithParameter);
}

void OnDisable()
{
    //Un-Register from onValueChanged Events
    dropdown.onValueChanged.RemoveAllListeners();
}

void callBack()
{

}

void callBackWithParameter(int value)
{

}

NON-UI OBJECTS:

6.For 3D Object (Mesh Renderer/any 3D Collider)

Add PhysicsRaycaster to the Camera then use any of the events from Method 1.

The code below will automatically add PhysicsRaycaster to the main Camera.

public class MeshDetector : MonoBehaviour, IPointerDownHandler
{
    void Start()
    {
        addPhysicsRaycaster();
    }

    void addPhysicsRaycaster()
    {
        PhysicsRaycaster physicsRaycaster = GameObject.FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    //Implement Other Events from Method 1
}

7.For 2D Object (Sprite Renderer/any 2D Collider)

Add Physics2DRaycaster to the Camera then use any of the events from Method 1.

The code below will automatically add Physics2DRaycaster to the main Camera.

public class SpriteDetector : MonoBehaviour, IPointerDownHandler
{
    void Start()
    {
        addPhysics2DRaycaster();
    }

    void addPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = GameObject.FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    //Implement Other Events from Method 1
}

Troubleshooting the EventSystem:

No clicks detected on UI, 2D Objects (Sprite Renderer/any 2D Collider) and 3D Objects (Mesh Renderer/any 3D Collider):

A.Check that you have EventSystem. Without EventSystem it can't detect clicks at-all. If you don't have have it, create it yourself.

Go to GameObject ---> UI ---> Event System. This will create an EventSystem if it doesn't exist yet. If it already exist, Unity will just ignore it.

B.The UI component or GameObject with the UI component must be under a Canvas. It means that a Canvas must be the parent of the UI component. Without this, EventSystem will not function and clicks will not be detected.

This only applies to UI Objects. It doesn't apply to 2D (Sprite Renderer/any 2D Collider) or 3D Objects (Mesh Renderer/any 3D Collider).

C.If this is a 3D Object, PhysicsRaycaster is not attached to the camera. Make sure that PhysicsRaycaster is attached to the camera. See #6 above for more information.

D.If this is a 2D Object, Physics2DRaycaster is not attached to the camera. Make sure that Physics2DRaycaster is attached to the camera. See #7 above for more information.

E.If this is a UI object you want to detect clicks on with the interface functions such as OnBeginDrag, OnPointerClick, OnPointerEnter and other functions mentioned in #1 then the script with the detection code must be attached to that UI Object you want to detect click on.

F.Also, if this is a UI Object you want to detect clicks on, make sure that no other UI Object is in front of it. If there is another UI in front of the one you want to detect click on, it will be blocking that click.

To verify that this is not the issue, disable every object under the Canvas except the one you want to detect click on then see if clicking it works.


*/
