using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public Transform OrgParent;
	public GameObject PlaceHolder;
	public Transform PlaceHoldPos;

	public bool IsPlayerCard = false;
	public bool IsDeckBuilding = false;

	public bool IsUsed = false;

	public void Start()
	{
		OrgParent = null;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (IsUsed == false)
		{
			GetComponent<CanvasGroup>().blocksRaycasts = false;
			PlaceHolder = new GameObject();
			PlaceHolder.transform.SetParent(this.transform.parent);
			PlaceHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

			if (this.transform.parent.GetComponent<DropZone>() != null)
			{
				if (this.transform.parent.GetComponent<DropZone>().IsDeckArea == true)
				{
					PlaceHoldPos = this.transform;
				}
			}

			OrgParent = this.transform.parent;

			if (this.transform.parent.GetComponent<DropZone>() != null)
			{
				this.transform.SetParent(this.transform.parent.parent.parent);
			}
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (IsUsed == false)
		{
			this.transform.position = eventData.position;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (IsUsed == false)
		{
			GetComponent<CanvasGroup>().blocksRaycasts = true;
			this.transform.SetParent(OrgParent);
			if (this.transform.parent.GetComponent<DropZone>() != null)
			{
				if (this.transform.parent.GetComponent<DropZone>().IsDeckArea == true)
				{
					this.transform.SetSiblingIndex(PlaceHolder.transform.GetSiblingIndex());
				}
			}
		}
		Destroy (PlaceHolder);
	}
}
