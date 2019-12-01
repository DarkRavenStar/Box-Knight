using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Deck", menuName = "Underdog/DeckData" )]
public class DeckData : ScriptableObject {

    public string deckname;
	public List<CardParent> cards = new List<CardParent> ();
}
