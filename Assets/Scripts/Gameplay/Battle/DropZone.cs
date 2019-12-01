using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
	public bool IsDeckArea;
	public Transform PlayerHand;
	public bool IsDone = false;

	[Header("Draw/Discard/Re-shuffle System")]
	public DrawDiscardReshuffle deckSystem;

	[Header("Status Effect System")]
	public StatusSystem playerStatusSystem;
	public StatusSystem enemyStatusSystem;

	[Header("AttackDefendHeal System")]
	public AttackDefendHeal Action;

	[Header("Stamina System")]
	public StaminaCounter PlayerStamina;

	[Header("PlayerElement UI System")]
	public ElementCall PlayerElement;

	[Header("Turn System")]
	public TurnSystem turnSystem;
	public ElementDmgCalculation elementCheck;



	public HealthCounter EnemyHP;
	int DamageData;

	public ArmourCounter Armour;
	int ArmourData;

	public StaminaCounter Stamina;
	int StaminaData = 0;

	public HealthCounter PlayerHealth;
	int HealingData;

	//public delegate void DropDelegate();
	//public static event DropDelegate OnDropEvent;
	//public GameObject Player;

	//public ElementDmgCalculation ECalculator;
	//public EnemyDisplay Enemy;

	public ShowDamage PlayerText;
	public ShowDamage EnemyText;

	//public StatusDamage status;

	//public ConditionCardExecute ConCardExecuter;
	//public BattleLog log;

	//public GameObject FireIcon;
	//public GameObject WaterIcon;
	//public GameObject EarthIcon;
	public PlayerData playerData;

	public bool InUse;

	//To GreyOut Cards
	[HideInInspector] public List<GameObject> CardsInHand;

	private Interactable CardData;
	private CardData CardInfo;
	private CardDisplay Card;

	public bool canStartActionEffect;
	public bool canStartStatusEffect;

	//CardData normalCard;
	//ConditionCardData conditionCard;
	public List<ActionCardEffect> actionCardEffect;
	public List<StatusCardEffect> statusCardEffect;

	public int actionSize;
	public int actionCount;

	public int statusSize;
	public int statusCount;

	public ActionCardEffect tempAction;
	public StatusCardEffect tempStatus;

	public ElementData elementData;
	public bool IsGuard = true;
	public bool UseGuardOnly = false;
	public List<Condition> conditions;
	public List<bool> canExecute;
	int numberOfCardUsed = 0;

	void Start()
	{
		//HP = HPCounter.GetComponent<HealthCounter>();
		//Armour = ArmourCounter.GetComponent<ArmourCounter>();
		//Stamina = StaminaCounter.GetComponent<StaminaCounter>();
		InUse = false;
		playerData = BattleSceneManager.Instance.GetPlayerData ();
	}

	void Update()
	{
		if (BattleSceneManager.Instance.IsPlayerTurn == true)
		{
			if (IsDeckArea == false)
			{
				if (this.transform.childCount > 0)
				{
					IsDone = false;
					foreach (Transform Child in this.transform)
					{
						CardData.transform.SetParent(PlayerHand);
						//Destroy (CardData.PlaceHolder);
						CardData.gameObject.SetActive(false);
						IsDone = true;
					}
				}
			}

			if (BattleSceneManager.Instance.CanPlayerPlay == false)
			{
				InUse = true;
				CoverUp();
			}

			if (BattleSceneManager.Instance.CanPlayerPlay == true)
			{
				if (canStartActionEffect == true)
				{
					ActionEffectExecute ();
				}

				if (canStartActionEffect == false && canStartStatusEffect == true)
				{
					StatusEffectExecute ();
				}

				if (canStartActionEffect == false && canStartStatusEffect == false)
				{
					InUse = false;
					Remove ();
				}
			}
		}

		if (BattleSceneManager.Instance.IsPlayerTurn == false)
		{
			InUse = true;
			CoverUp();
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (BattleSceneManager.Instance.IsPlayerTurn == true)
		{
			CardData = eventData.pointerDrag.GetComponent<Interactable>();
			//CardInfo = eventData.pointerDrag.GetComponent<CardData>();
			Card = CardData.GetComponent<CardDisplay>();

			if (CardData != null)
			{
				if (IsDeckArea == false)
				{
					if (InUse == false)
					{
						numberOfCardUsed++;
						if (Card.card.staminaCost <= Stamina.currentStamina)
						{
							CardData.OrgParent = this.transform;
							//CardData.transform.SetParent(PlayerHand);
							CardData.transform.SetAsLastSibling();
							//Destroy (CardData.PlaceHolder);
							//CardData.gameObject.SetActive(false);

							if (Card.card.IsConditionCard == false)
							{
								CardData normalCard = (CardData)Card.card;

								if (normalCard.actionEffect.Count > 0)
								{
									actionCount = 0;
									actionCardEffect = normalCard.actionEffect;
									actionSize = actionCardEffect.Count;
									if (actionSize > 0 && actionCardEffect != null) {canStartActionEffect = true;}
								}

								if (normalCard.statusEffect.Count > 0)
								{
									statusCount = 0;
									statusCardEffect = normalCard.statusEffect;
									statusSize = statusCardEffect.Count;
									if (statusSize > 0 && statusCardEffect != null) {canStartStatusEffect = true;}
								}

								elementData = normalCard.element;
								StaminaData = Card.card.staminaCost;
								PlayerStamina.StaminaDecrease (StaminaData);
							}
						}
						if (Card.card.IsConditionCard == true)
						{
							ConditionCardData normalCard = (ConditionCardData)Card.card;
							Execute (normalCard);
							actionSize = actionCardEffect.Count;
							if (actionSize > 0 && actionCardEffect != null) {
								canStartActionEffect = true;
							}
						}
						else if(Card.card.staminaCost > Stamina.currentStamina)
						{
							Card.Blink();
							PlayerText.NoStamina();
							CardData.transform.SetSiblingIndex(CardData.PlaceHoldPos.GetSiblingIndex());
						}
					}
				}
				else if (IsDeckArea == true)
				{
					//Return it to original position before it was placed in the other dropzone
					CardData.transform.SetSiblingIndex(CardData.PlaceHoldPos.GetSiblingIndex());
					//Debug.Log(CardData.name + " Was Not Used");
				}
			}
		}                   
	}

	public void Execute(ConditionCardData conditionCard)
	{
		canExecute.Clear ();
		CheckCondition (conditionCard.conditions);

		if (IsAllGood () == true) {
			actionCardEffect = conditionCard.successConditionEffects;
			Debug.Log ("All Good");
		}
		else
		{
			actionCardEffect = conditionCard.failedConditionEffects;
			Debug.Log ("Not Good");
		}
	}

	public void CheckCondition(List<Condition> conditions)
	{
		for (int i = 0; i < conditions.Count; i++)
		{
			if (conditions[i].conditionType == ConditionType.PLAYER_HP)
			{
				ValueCheck (conditions[i], PlayerHealth.CurrentHealth);
			}
			if (conditions[i].conditionType == ConditionType.NUM_OF_TURNS)
			{
				ValueCheck (conditions[i], turnSystem.counter);
			}

			if (conditions[i].conditionType == ConditionType.NUM_OF_CARDS_USED)
			{
				ValueCheck (conditions[i], numberOfCardUsed);
			}

			if (conditions[i].conditionType == ConditionType.ELEMENT_APPLIED)
			{
				ValueCheckElement (conditions[i], elementCheck.Element());
			}
		}
	}

	public void ValueCheck(Condition con, int checkValue)
	{
		if (con.conditionComparison == ConditionComparison.EQUAL_TO) 
		{
			if (checkValue == con.value) {
				canExecute.Add (true);
			}
			else
			{
				canExecute.Add (false);
			}
		}

		if (con.conditionComparison == ConditionComparison.LESS_OR_EQUAL_TO) 
		{
			if (checkValue <= con.value) {
				canExecute.Add (true);
			}
			else
			{
				canExecute.Add (false);
			}
		}

		if (con.conditionComparison == ConditionComparison.LESS_THAN) 
		{
			if (checkValue < con.value) {
				canExecute.Add (true);
			}
			else
			{
				canExecute.Add (false);
			}
		}

		if (con.conditionComparison == ConditionComparison.MORE_OR_EQUAL_TO) 
		{
			if (checkValue >= con.value) {
				canExecute.Add (true);
			}
			else
			{
				canExecute.Add (false);
			}
		}

		if (con.conditionComparison == ConditionComparison.MORE_THAN) 
		{
			if (checkValue > con.value) {
				canExecute.Add (true);
			}
			else
			{
				canExecute.Add (false);
			}
		}

		if (con.conditionComparison == ConditionComparison.ODD) 
		{
			if (checkValue % con.value != 0) {
				canExecute.Add (true);
			}
			else
			{
				canExecute.Add (false);
			}
		}

		if (con.conditionComparison == ConditionComparison.EVEN) 
		{
			if (checkValue % con.value == 0) {
				canExecute.Add (true);
			}
			else
			{
				canExecute.Add (false);
			}
		}
	}

	public void ValueCheckElement(Condition con, ElementType element)
	{
		if (element != ElementType.NONE)  {
			canExecute.Add (true);
		}
		else
		{
			canExecute.Add (false);
		}
	}

	public bool IsAllGood()
	{
		if (canExecute.Count > 0) {
			for (int i = 0; i < canExecute.Count; i++) {
				if (canExecute [i] == false) {
					return false;
				}
			}
			return true;
		} else {
			return false;
		}
	}

	public void ActionEffectExecute()
	{
		if (actionCount < actionSize && actionCardEffect.Count > 0)
		{
			tempAction = actionCardEffect [actionCount];

			if (UseGuardOnly == false)
			{
				if (tempAction.actionType == ActionType.DISCARD) { deckSystem.SetCardDrawDiscardReshuffleSetting (DrawType.DISCARD, 0, tempAction.num); }

				if (tempAction.actionType == ActionType.DRAW) { deckSystem.SetCardDrawDiscardReshuffleSetting (DrawType.DRAW, tempAction.num, 0); }

				if (tempAction.actionType == ActionType.RESHUFFLE) { deckSystem.SetCardDrawDiscardReshuffleSetting (DrawType.RESHUFFLE, tempAction.num, tempAction.num); }


				if (tempAction.actionType == ActionType.BOOST)
				{
					if(elementData.elements == ElementType.EARTH) PlayerElement.CallEarth ();
					if(elementData.elements == ElementType.WATER) PlayerElement.CallWater ();
					if(elementData.elements == ElementType.FIRE)  PlayerElement.CallFire ();

					Action.GetCardData (actionCardEffect [actionCount], elementData);
				}

				if (tempAction.actionType == ActionType.CHARGE)
				{
					StaminaData = tempAction.num;
					PlayerStamina.StaminaIncrease (StaminaData);
				}

				if (tempAction.actionType == ActionType.ENHANCE)
				{
					Action.GetCardData (actionCardEffect [actionCount], elementData);
				}

				if (tempAction.actionType == ActionType.HEAL)
				{
					Action.GetCardData (actionCardEffect [actionCount], elementData);
				}

				if (tempAction.actionType == ActionType.STRIKE)
				{
					Action.GetCardData (actionCardEffect [actionCount], elementData);
				}
			}


			if (tempAction.actionType == ActionType.GUARD)
			{
				Action.GetCardData (actionCardEffect [actionCount], elementData);
			}
			actionCount++;
		}
		else if(actionCount == actionSize)
		{
			BattleSceneManager.Instance.CanPlayerPlay = true;
			canStartActionEffect = false;
		}
	}

	public void StatusEffectExecute()
	{
		if (statusCount < statusSize && statusCardEffect.Count > 0)
		{
			tempStatus = statusCardEffect [statusCount];

			//Negative status
			if (tempStatus.statusType == StatusType.BLEED)
			{
				Debug.Log (tempStatus.statusType.ToString ());
				enemyStatusSystem.StatusApplied (statusCardEffect [statusCount]);
				//enemyStatusSystem.StatusApplied (statusCardEffect [statusCount]);
			}

			if (tempStatus.statusType == StatusType.POISON)
			{
				Debug.Log (tempStatus.statusType.ToString ());
				enemyStatusSystem.StatusApplied (statusCardEffect [statusCount]);
				//enemyStatusSystem.StatusApplied (statusCardEffect [statusCount]);
			}

			if (tempStatus.statusType == StatusType.STUN)
			{
				Debug.Log (tempStatus.statusType.ToString ());
				enemyStatusSystem.StatusApplied (statusCardEffect [statusCount]);
				//enemyStatusSystem.StatusApplied (statusCardEffect [statusCount]);
			}

			if (tempStatus.statusType == StatusType.ELEMENTLESS)
			{
				Debug.Log (tempStatus.statusType.ToString ());
				enemyStatusSystem.StatusApplied (statusCardEffect [statusCount]);
				//enemyStatusSystem.StatusApplied (statusCardEffect [statusCount]);
			}

			if (tempStatus.statusType == StatusType.FATIGUE)
			{
				Debug.Log (tempStatus.statusType.ToString ());
				enemyStatusSystem.StatusApplied (statusCardEffect [statusCount]);
				//enemyStatusSystem.StatusApplied (statusCardEffect [statusCount]);
			}

			//Positve status
			if (tempStatus.statusType == StatusType.COUNTER)
			{
				Debug.Log (tempStatus.statusType.ToString ());
				if (IsGuard == true)
				{
					playerStatusSystem.StatusApplied (statusCardEffect [statusCount]);
				}
			}

			if (tempStatus.statusType == StatusType.HEALTH_REGEN)
			{
				Debug.Log (tempStatus.statusType.ToString ());
				playerStatusSystem.StatusApplied (statusCardEffect [statusCount]);
				//playerStatusSystem.StatusApplied (statusCardEffect [statusCount]);
			}

			if (tempStatus.statusType == StatusType.RESISTANCE)
			{
				Debug.Log (tempStatus.statusType.ToString ());
				playerStatusSystem.StatusApplied (statusCardEffect [statusCount]);
				//playerStatusSystem.StatusApplied (statusCardEffect [statusCount]);
			}
			statusCount++;
		}
		else if(statusCount == statusSize)
		{
			canStartStatusEffect = false;
		}
	}

	public void CoverUp()
	{
		CardsInHand.Clear();
		for(int i=0; i<PlayerHand.childCount; i++)
		{
			if(PlayerHand.GetChild(i).gameObject.activeInHierarchy == true)
			{
				if(PlayerHand.GetChild(i).gameObject != null)
				{
					CardsInHand.Add(PlayerHand.GetChild(i).gameObject);
				}
			}
		}
		for(int i=0; i<CardsInHand.Count; i++)
		{
			if (CardsInHand[i].GetComponent<CardDisplay>() != null)
			{
				CardsInHand[i].GetComponent<Interactable>().IsUsed = true;
				CardsInHand[i].GetComponent<CardDisplay>().GreyOut();
			}
		}
	}

	public void Remove()
	{
		CardsInHand.Clear();
		for (int i = 0; i < PlayerHand.childCount; i++)
		{
			if (PlayerHand.GetChild(i).gameObject.activeInHierarchy == true)
			{
				if (PlayerHand.GetChild(i).gameObject != null)
				{
					CardsInHand.Add(PlayerHand.GetChild(i).gameObject);
				}
			}
		}
		for (int i = 0; i < CardsInHand.Count; i++)
		{
			if (CardsInHand[i].GetComponent<CardDisplay>() != null)
			{
				CardsInHand[i].GetComponent<Interactable>().IsUsed = false;
				CardsInHand[i].GetComponent<CardDisplay>().Restore();
			}
		}
	}
}
