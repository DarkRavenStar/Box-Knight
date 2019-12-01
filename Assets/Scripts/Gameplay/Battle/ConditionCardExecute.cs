//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//public class ConditionCardExecute : MonoBehaviour {
//	
//	public Transform playerHand;
//	public DeckData PlayerDeck;
//
//	public DrawDiscardReshuffle deckSystem;
//
//	public List<Condition> conditions = new List<Condition> ();
//	public List<ActionCardEffect> effects = new List<ActionCardEffect>();
//	public StaminaCounter stamina;
//	public PlayerHealth playerHealth;
//	//public ConditionCardData conditionCards;
//	public List<GameObject> spareCards;
//	public List<GameObject> actionCards;
//	public List<bool> canExecute = new List<bool>();
//
//	public void Execute(ConditionCardData conditionCard, bool IsDone)
//	{
//		canExecute.Clear ();
//		CheckCondition (conditionCard.conditions);
//
//		if (IsAllGood () == true) {
//			RunEffects (conditionCard.successConditionEffects, IsDone);
//			Debug.Log ("All Good");
//		}
//		else
//		{
//			RunEffects (conditionCard.failedConditionEffects, IsDone);
//			Debug.Log ("Not Good");
//		}
//	}
//
//	public void CheckCondition(List<Condition> conditions)
//	{
//		for (int i = 0; i < conditions.Count; i++)
//		{
//			if (conditions[i].conditionType == ConditionType.PLAYER_HP)
//			{
//				ValueCheck (conditions[i], BattleLog.Instance.playerHealth);
//			}
//			if (conditions[i].conditionType == ConditionType.NUM_OF_TURNS)
//			{
//				ValueCheck (conditions[i], BattleLog.Instance.numberOfTurns);
//			}
//
//			if (conditions[i].conditionType == ConditionType.NUM_OF_CARDS_USED)
//			{
//				ValueCheck (conditions[i], BattleLog.Instance.numberOfCardsUsed);
//			}
//
//			if (conditions[i].conditionType == ConditionType.ELEMENT_APPLIED)
//			{
//				ValueCheckElement (conditions[i], BattleLog.Instance.elementType);
//			}
//		}
//	}
//
//	public void ValueCheck(Condition con, int checkValue)
//	{
//		if (con.conditionComparison == ConditionComparison.EQUAL_TO) 
//		{
//			if (checkValue == con.value) {
//				canExecute.Add (true);
//			}
//			else
//			{
//				canExecute.Add (false);
//			}
//		}
//
//		if (con.conditionComparison == ConditionComparison.LESS_OR_EQUAL_TO) 
//		{
//			if (checkValue <= con.value) {
//				canExecute.Add (true);
//			}
//			else
//			{
//				canExecute.Add (false);
//			}
//		}
//
//		if (con.conditionComparison == ConditionComparison.LESS_THAN) 
//		{
//			if (checkValue < con.value) {
//				canExecute.Add (true);
//			}
//			else
//			{
//				canExecute.Add (false);
//			}
//		}
//
//		if (con.conditionComparison == ConditionComparison.MORE_OR_EQUAL_TO) 
//		{
//			if (checkValue >= con.value) {
//				canExecute.Add (true);
//			}
//			else
//			{
//				canExecute.Add (false);
//			}
//		}
//
//		if (con.conditionComparison == ConditionComparison.MORE_THAN) 
//		{
//			if (checkValue > con.value) {
//				canExecute.Add (true);
//			}
//			else
//			{
//				canExecute.Add (false);
//			}
//		}
//
//		if (con.conditionComparison == ConditionComparison.ODD) 
//		{
//			if (checkValue % con.value != 0) {
//				canExecute.Add (true);
//			}
//			else
//			{
//				canExecute.Add (false);
//			}
//		}
//
//		if (con.conditionComparison == ConditionComparison.EVEN) 
//		{
//			if (checkValue % con.value == 0) {
//				canExecute.Add (true);
//			}
//			else
//			{
//				canExecute.Add (false);
//			}
//		}
//	}
//
//	public void ValueCheckElement(Condition con, ElementType element)
//	{
//		if (element != ElementType.NONE)  {
//			canExecute.Add (true);
//		}
//		else
//		{
//			canExecute.Add (false);
//		}
//	}
//
//	public bool IsAllGood()
//	{
//		if (canExecute.Count > 0) {
//			for (int i = 0; i < canExecute.Count; i++) {
//				if (canExecute [i] == false) {
//					return false;
//				}
//			}
//			return true;
//		} else {
//			return false;
//		}
//	}
//
//	public void RunEffects(List<ActionCardEffect> effects, bool IsDone)
//	{
//		foreach (ActionCardEffect effect in effects)
//		{
//			ActionCheck (effect.actionType, effect.num, IsDone);
//		}
//
//	}
//
//	public void ActionCheck(ActionType actions, int effectValue, bool IsDone)
//	{
//		if (actions == ActionType.DRAW)
//		{
//			DrawCards (effectValue);
//			Debug.Log ("Draw");
//		}
//
//		if (actions == ActionType.CHARGE_MAX)
//		{
//			if (BattleLog.Instance.stamina < BattleLog.Instance.staminaLimit)
//			{
//				BattleLog.Instance.stamina = BattleLog.Instance.staminaLimit;
//				stamina.currentStamina = BattleLog.Instance.staminaLimit;
//				stamina.UpdateStamina ();
//				Debug.Log (stamina.currentStamina);
//				Debug.Log ("Stamina Maxed");
//			}
//		}
//
//		if (actions == ActionType.CHARGE)
//		{
//			stamina.currentStamina += effectValue;
//			stamina.UpdateStamina ();
//			Debug.Log ("Stamina Added" + effectValue);
//		}
//
//		if (actions == ActionType.DAMAGE_SELF)
//		{
//			playerHealth.playerHealth -= effectValue;
//			playerHealth.healthText.text = playerHealth.ToString ();
//			Debug.Log ("DAMAGE_SELF" + effectValue);
//		}
//
//		if (actions == ActionType.GUARD) {
//			playerHealth.ArmourValue.Armour += effectValue;
//			playerHealth.ArmourValue.UpdateArmour ();
//			Debug.Log ("GUARD" + effectValue);
//		}
//
//		if (actions == ActionType.HEAL) {
//			playerHealth.playerHealth += effectValue;
//			playerHealth.healthText.text = playerHealth.ToString ();
//			Debug.Log ("HEAL" + effectValue);
//		}
//
//		if (actions == ActionType.DISCARD) {
//			RemoveCards (effectValue);
//			Debug.Log ("DISCARD" + effectValue);
//		}
//	}
//}