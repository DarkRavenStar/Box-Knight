using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Interactable1 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Transform OrgParent;
    public GameObject PlaceHolder;

    public Transform PlaceHoldPos;

    public bool IsPlayerCard = false;
	public bool IsDeckBuilding = false;

    public void Start()
    {
        OrgParent = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
		//Debug.Log ("IsDeckBuilding" + IsDeckBuilding);
		//Debug.Log("Grab");

        this.GetComponent<TapForInfo>().InfoOn = false;
        GameObject infoPanel = GameObject.FindGameObjectWithTag("CardInfoPanel");
        if(infoPanel != null)
        {
            infoPanel.transform.GetChild(0).gameObject.SetActive(false);
        }

        PlaceHolder = new GameObject();
        PlaceHolder.transform.SetParent(this.transform.parent);
        PlaceHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        if (this.transform.parent.GetComponent<DeckZone>() != null)
        {
            PlaceHoldPos = this.transform;
        }

        //Shows what is the selected card's sibling index
        //Debug.Log(this.transform.GetSiblingIndex());
        //Debug.Log(PlaceHolder.transform.GetSiblingIndex());

        OrgParent = this.transform.parent;

		if (IsDeckBuilding == true)
		{
			this.transform.SetParent (this.transform.parent.parent.parent);
		}
		else
		{
			if (this.transform.parent.GetComponent<DeckZone>() != null)
			{
				this.transform.SetParent(this.transform.parent.parent);
			}
		}

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");

        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Drop");

        this.transform.SetParent(OrgParent);

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (this.transform.parent.GetComponent<DeckZone>() != null)
        {
            this.transform.SetSiblingIndex(PlaceHolder.transform.GetSiblingIndex());
            IsPlayerCard = OrgParent.GetComponent<DeckZone>().playerZone;
        }
        //Debug.Log(this.transform.GetSiblingIndex());

        Destroy(PlaceHolder);
        //Save Placeholder Data
    }
}
