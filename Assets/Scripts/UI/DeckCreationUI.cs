using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckCreationUI : MonoBehaviour {
	public DeckData deck;
	public GameObject card;
	public Transform panel;

	void Start()
	{
		for (int i = 0; i < deck.cards.Count; i++) {
			GameObject tempCard = Instantiate (card, panel);
			tempCard.transform.SetParent (panel);
			if (tempCard.GetComponent<CardDisplay> () != null) {
				tempCard.GetComponent<CardDisplay> ().card = deck.cards [i];
				tempCard.GetComponent<Interactable1> ().IsPlayerCard = this.GetComponent<DeckZone>().playerZone;
				tempCard.GetComponent<Interactable1> ().IsDeckBuilding = true;
			}
		}
	}

	public void Refresh()
	{
		for (int i = 0; i < deck.cards.Count; i++) {
			Destroy (transform.GetChild (i).gameObject);
			GameObject tempCard = Instantiate (card, panel);
			tempCard.transform.SetParent (panel);
			if (tempCard.GetComponent<CardDisplay> () != null) {
				tempCard.GetComponent<CardDisplay> ().card = deck.cards [i];
				tempCard.GetComponent<Interactable1> ().IsPlayerCard = this.GetComponent<DeckZone>().playerZone;
				tempCard.GetComponent<Interactable1> ().IsDeckBuilding = true;
			}
		}
	}


}
