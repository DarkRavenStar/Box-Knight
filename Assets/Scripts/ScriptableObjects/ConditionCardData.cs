using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ConditionType
{
	PLAYER_HP,
	PLAYER_ARMOR,
	ENEMY_HP,
	ENEMY_ARMOR,
	NUM_OF_CARDS_USED,
	NUM_OF_TURNS,
	ELEMENT_APPLIED,
}

[System.Serializable]
public enum ConditionComparison
{
	LESS_THAN,
	LESS_OR_EQUAL_TO,
	MORE_THAN,
	MORE_OR_EQUAL_TO,
	EQUAL_TO,
	ODD,
	EVEN,
}

[System.Serializable]
public class Condition
{
	public ConditionType conditionType;
	public ConditionComparison conditionComparison;
	public int value;
}

[System.Serializable]
public class StatusCardEffect
{
	public StatusType statusType;
	public int numOfTurns;
}

[System.Serializable]
public class ActionCardEffect
{
	public ActionType actionType;
	public int num;
}



[CreateAssetMenu(fileName = "New Condition Card", menuName = "Underdog/ConditionCardData" )]
public class ConditionCardData : CardParent {

	public List<Condition> conditions;
	public List<ActionCardEffect> failedConditionEffects;
	public List<StatusCardEffect> failedConditionStatus;
	public List<ActionCardEffect> successConditionEffects;
	public List<StatusCardEffect> successConditionStatus;
}
