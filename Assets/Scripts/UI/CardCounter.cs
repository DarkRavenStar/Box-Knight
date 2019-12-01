using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardCounter : MonoBehaviour {

	public bool IsPlayerHandCounter;
	public Text text;
	public Transform panel;
	public int counter;
	public int cardLimit = 20;
	public DeckZone finaliseDeck;

	public void CountCard()
	{
		counter = transform.childCount;
	}

	public void DisplayCount()
	{
		if (IsPlayerHandCounter == false) {
			text.text = counter.ToString () + " cards left";
		}
		else if (IsPlayerHandCounter == true) {
			int cardLeft = cardLimit - counter;
			text.text = cardLeft.ToString () + " cards needed";
		}
		finaliseDeck.GetComponent<DeckZone> ().FinalizeCard ();
	}

	public void Completed()
	{
		if (counter == 20) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
		/*
		if(IsPlayerHandCounter == true)
		{
			if (completed == true) {
				SceneManager.LoadScene(1);
			}
		}
		*/

		/*
		if (counter == 20) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			Debug.Log("Deck Building Completed");
		}
		*/
	}

	void Update()
	{
		CountCard ();
		DisplayCount ();
	}
}
