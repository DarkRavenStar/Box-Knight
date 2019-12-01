using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum DrawType
{
	DISCARD,
	DRAW,
	RESHUFFLE,
};

public class DrawDiscardReshuffle : MonoBehaviour {

	[SerializeField] int DrawAmount;
	[SerializeField] int DiscardAmount;
	[SerializeField] float Speed;

	[Space(10)]
	[SerializeField] Transform Destination;
	[SerializeField] Transform PlayerHand;
	[SerializeField] Transform Deck;
	[SerializeField] Transform Origin;

	[Space(10)]

	[SerializeField] HaloGlow DeckGlow;
	[SerializeField] Vector3 DeckScale;


	[Header("Scaling for player hand and vise versa")]
	[SerializeField] Vector3 CardDeckScaleToPlayerHand;
	[SerializeField] Vector3 PlayerCardScaleToCardDeck;

	[SerializeField] bool shuffled;
	[SerializeField] bool LocationSet;
	[SerializeField] bool Completed;
	[SerializeField] bool EndTurn = true;
	[SerializeField] bool DrawLater = true;

	/*[SerializeField]*/ public DeckData PlayerDeck;
	[SerializeField] List<CardParent> TempList;

	[SerializeField] List<GameObject> activeCards;
	[SerializeField] List<GameObject> inactiveCards;

	[SerializeField] DrawType drawType;

    private Vector3 OriginSpawn;
    private Vector3 OriginScale;
	private Vector3 Target = Vector3.zero;

	private int DeckSize;
	private float FracJourney;
	private CardDisplay Card; //Change Card Image
	private int CardCount = 0;
	private int currentDeckIndex = 0;

	// Use this for initialization
	void Start () {
        //Origin = this.transform;
		OriginSpawn = Origin.transform.position;
		OriginScale = Origin.transform.localScale;
		 
        LocationSet = false;
        Completed = false;
		//RectTransformUtility.ScreenPointToWorldPointInRectangle

        Target = new Vector3(PlayerHand.position.x, PlayerHand.position.y + 150);
		Card = Origin.GetComponent<CardDisplay>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (BattleSceneManager.Instance.IsPlayerTurn == true)
		{
			if (Completed == false)
			{
				BattleSceneManager.Instance.CanPlayerPlay = false;
				if (drawType == DrawType.DRAW)
				{
					DrawCards (DrawAmount, EndTurn);
				}
			}
		}

		if (drawType == DrawType.DISCARD)
		{
			DiscardCards (DiscardAmount, DrawLater, EndTurn);
		}
    }

	public void ActivateDrawFunction()
    {
		Origin.gameObject.SetActive(true);
		drawType = DrawType.DISCARD;
		CardCount = 0;
		DrawAmount = 5;
		DrawLater = true;
		EndTurn = true;

		shuffled = false;
		Shuffle (PlayerDeck, TempList);

		LocationSet = false;
		Completed = false;
    }

	public void ActivateDraw()
	{
		Origin.gameObject.SetActive(true);
		drawType = DrawType.DRAW;
		CardCount = 0;
		currentDeckIndex = 0;
		DrawAmount = 5;
		EndTurn = true;

		shuffled = false;
		Shuffle (PlayerDeck, TempList);

		LocationSet = false;
		Completed = false;
	}

	public void ActivateDiscard()
	{
		Origin.gameObject.SetActive(true);
		drawType = DrawType.DISCARD;
		CardCount = 0;
		DrawAmount = 5;
		currentDeckIndex = 0;
		DrawLater = false;
		EndTurn = true;

		shuffled = false;
		Shuffle (PlayerDeck, TempList);

		LocationSet = false;
		Completed = false;
	}

	public void SetCardDrawDiscardReshuffleSetting(DrawType type, int addCards, int removeCards)
	{
		if (type == DrawType.DISCARD)
		{
			drawType = type;
			DrawLater = false;
			DiscardAmount = removeCards;
		}

		if (type == DrawType.DRAW)
		{
			drawType = type;
			DrawLater = false;
			DrawAmount = addCards;
		}

		if (type == DrawType.RESHUFFLE)
		{
			drawType = DrawType.DISCARD;
			DrawLater = true;
			DrawAmount = addCards;
			DiscardAmount = removeCards;
		}

		CardCount = 0;
		EndTurn = false;
		Origin.gameObject.SetActive(true);
		LocationSet = false;
		Completed = false;
	}

	void DrawCards(int Limit, bool IsEndTurn)
	{
		if (inactiveCards.Count == 0)
		{
			GetInactiveCards (inactiveCards);
		}
		if (Limit > inactiveCards.Count)
		{
			Limit = inactiveCards.Count;
		}
		if (CardCount < Limit && inactiveCards.Count > 0)
		{
			Card.card = TempList[CardCount+currentDeckIndex];
			Card.Restart();
			if (LocationSet == false)
			{
				FracJourney += Speed;
				Origin.position = Vector3.Lerp(Origin.position, Target, FracJourney);
				Origin.localScale = Vector3.Lerp(Origin.localScale, CardDeckScaleToPlayerHand, FracJourney);
				DeckGlow.image.enabled = true;
				if (FracJourney > 0.5f)
				{
					Deck.localScale = Vector3.Lerp(DeckScale, Deck.localScale, FracJourney);
				}
				else if (FracJourney <= 0.5f)
				{
					Deck.localScale = Vector3.Lerp(Deck.localScale, DeckScale, FracJourney);
				}
				if (Origin.position.y == Target.y)
				{
                    if (Origin.position.x > Target.x - 2 && Origin.position.x < Target.x + 2)
                    {
                        Origin.position = OriginSpawn;
                        Origin.localScale = OriginScale;
                        Deck.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        FracJourney = 0.0f;
                        ActivateCard(inactiveCards[CardCount], TempList[CardCount]);
                        CardCount++;

                        if (IsEndTurn == false)
                        {
                            currentDeckIndex++;
                            if (currentDeckIndex == TempList.Count) currentDeckIndex = 0;
                        }

                        if (CardCount >= Limit)
                        {
                            LocationSet = true;
                            Completed = true;
                            DeckGlow.image.enabled = false;
                            Origin.gameObject.SetActive(false);
                            BattleSceneManager.Instance.CanPlayerPlay = true;
                            if (IsEndTurn == true)
                            {
                                IsEndTurn = false;
                                currentDeckIndex += 5;
                            }
                            inactiveCards.Clear();
                        }
                    }
                }
			}
		}
	}

	void DiscardCards(int Limit, bool IsDraw, bool IsEndTurn)
	{
		if (activeCards.Count == 0)
		{
			GetActiveCards (activeCards);
		}
		if (Limit > activeCards.Count)
		{
			Limit = activeCards.Count;
		}
		if (IsEndTurn == true)
		{
			Limit = activeCards.Count;
		}

		if (CardCount < Limit && activeCards.Count > 0) {
			if (LocationSet == false) {
				Card.card = activeCards [CardCount].GetComponent<CardDisplay> ().card;
				if (Card.card != null) {
					Card.Restart ();
					DeactivateCard (activeCards [CardCount]);
				}

				FracJourney += Speed * 2;

				Origin.position = Vector3.Lerp (Target, Deck.position, FracJourney);
				Origin.localScale = Vector3.Lerp (PlayerHand.localScale, PlayerCardScaleToCardDeck, FracJourney);
				DeckGlow.image.enabled = true;

				if (FracJourney > 0.5f) {
					Deck.localScale = Vector3.Lerp (DeckScale, Deck.localScale, FracJourney);
				} else if (FracJourney <= 0.5f) {
					Deck.localScale = Vector3.Lerp (Deck.localScale, DeckScale, FracJourney);
				}

				if (Origin.position == Deck.position) {
					Origin.position = Target;
					Origin.localScale = PlayerHand.localScale;
					Deck.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
					FracJourney = 0.0f;
					CardCount++;

					if (CardCount >= Limit)
					{
						Origin.position = OriginSpawn;
						Origin.localScale = OriginScale;

						DeckGlow.image.enabled = false;
						BattleSceneManager.Instance.CanPlayerPlay = true;
						CardCount = 0;
						if (IsDraw == true) 
						{
							drawType = DrawType.DRAW;
						}
						else
						{
							Origin.gameObject.SetActive(false);	
							LocationSet = true;
							Completed = true;

							if (EndTurn == true)
							{
								BattleSceneManager.Instance.IsPlayerTurn = false;
								BattleSceneManager.Instance.CanPlayerPlay = false;
								BattleSceneManager.Instance.CanEnemyPlay = true;
							}
						}
						activeCards.Clear ();
					}
				}
			}
		}
		else
		{
			if (IsDraw == true)
				drawType = DrawType.DRAW;
		}
	}

	public void GetInactiveCards(List<GameObject> unused)
	{
		unused.Clear ();
		int AllCards = PlayerHand.childCount;
		for (int i = 0; i < AllCards; i++) {
			if (PlayerHand.GetChild (i).gameObject.activeSelf == false) {
				unused.Add (PlayerHand.GetChild (i).gameObject);
			}
		}
	}

	public void GetActiveCards(List<GameObject> unused)
	{
		unused.Clear ();
		int AllCards = PlayerHand.childCount;
		for (int i = 0; i < AllCards; i++) {
			if (PlayerHand.GetChild (i).gameObject.activeSelf == true) {
				unused.Add (PlayerHand.GetChild (i).gameObject);
			}
		}
	}

	void ActivateCard(GameObject card, CardParent cardParent)
	{
		if (card.gameObject.activeSelf == false)
		{
			card.gameObject.SetActive(true);
			card.GetComponent<CardDisplay> ().card = cardParent;
			card.GetComponent<CardDisplay> ().Restart ();
		}
	}

	void DeactivateCard(GameObject card)
	{
		if (card.gameObject.activeSelf == true)
		{
			card.gameObject.SetActive(false);
		}
	}

	void GetCardData(GameObject tempCard, CardParent card)
	{
		card = tempCard.GetComponent<CardDisplay> ().card;
	}

	void Shuffle(DeckData originDeck, List<CardParent> resultDeckList)
	{
		resultDeckList.Clear ();
		if (shuffled == false && originDeck != null)
		{
			shuffled = true;
			int size = originDeck.cards.Count;

			//Sets up Temp Deck
			for (int i = 0; i < size; i++)
			{
				resultDeckList.Add(originDeck.cards[i]);
			}

			//Shuffle Deck
			for(int i=0; i< size; i++)
			{
				bool Done = false;
				do
				{
					int Source = Random.Range(0, size - 1);
					int Target = Random.Range(0, size - 1);
					if (Source != Target)
					{
						CardParent PlaceHolder = resultDeckList[Target];
						resultDeckList[Target] = resultDeckList[Source];
						resultDeckList[Source] = PlaceHolder;
						Done = true;
						shuffled = false;
					}
				} while (Done != true);
			}
		}
	}

}
