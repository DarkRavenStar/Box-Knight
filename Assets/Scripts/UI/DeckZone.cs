using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckZone : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler{

	private Interactable1 CardData;
	public bool IsDeckArea;
	public DeckData deckList;
	public bool playerZone;
	public const int cardLimit = 20;
	//public bool IsPlayerCard;

	void Start()
	{
		deckList = this.GetComponent<DeckCreationUI> ().deck;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log("Something Entered");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//Debug.Log("Something Exited");
	}

	public void OnDrop(PointerEventData eventData)
	{
		CardData = eventData.pointerDrag.GetComponent<Interactable1>();
		if (CardData != null) {
			bool IsPlayerCard = CardData.IsPlayerCard;
			if (CardData != null)
			{
				if (playerZone == IsPlayerCard)
				{
					//Return it to original position before it was placed in the other dropzone
					CardData.transform.SetSiblingIndex(CardData.PlaceHoldPos.GetSiblingIndex());
					//FinalizeCard ();
				}

				if (playerZone != IsPlayerCard)
				{
					if (GetComponent<CardCounter> ().IsPlayerHandCounter == true) {
						if (GetComponent<CardCounter> ().counter < cardLimit) {
							CardData.OrgParent = this.transform;
							//Puts card at the far right of the dropzone
							CardData.transform.SetAsLastSibling ();
						} else {
							CardData.transform.SetSiblingIndex(CardData.PlaceHoldPos.GetSiblingIndex());
						}
					}
					else
					{
						CardData.OrgParent = this.transform;
						//Puts card at the far right of the dropzone
						CardData.transform.SetAsLastSibling ();
					}
					//FinalizeCard ();
				}
			}
		}
	}

	public void FinalizeCard ()
	{
		deckList.cards.Clear ();
		for (int i = 0; i < this.transform.childCount; i++) {
			CardParent tempCard = this.transform.GetChild (i).GetComponent<CardDisplay> ().card;
			AddCard (tempCard);
		}
	}

	public void AddCard(CardParent card)
	{
		deckList.cards.Add (card);
		//transform.childCount
	}

	public void RemoveCard(CardParent card)
	{
		deckList.cards.Remove (card);
	}
}
