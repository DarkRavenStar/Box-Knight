using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ActionType
{
	[Header("Attack damage")]
	STRIKE,

	[Header("Recovery")]
	HEAL,

	[Header("Armour")]
	GUARD,

	[Header("Element Cloak")]
	BOOST,

	[Header("Add cards from deck to hand")]
	DRAW,

	[Header("Return cards to deck from hand and add cards from deck to hand.")]
	RESHUFFLE,

	[Header("Remove card")]
	DISCARD,

	[Header("Recover stamina")]
	CHARGE,

	[Header("Apply Element to attack")]
	ENHANCE,

	[Header("")]
	CHARGE_MAX,

	[Header("")]
	DAMAGE_ADD,

	[Header("")]
	DAMAGE_SELF

}


[System.Serializable]
public enum StatusType
{
	[Header("Every action -1 hp, last for 3 turns. \n After gain immunity to bleed for 3 turns.")]
	BLEED,

	[Header("Every start of turn -3 hp, last for 3 turns.\n After gain immunity to poison for 3 turns.")]
	POISON,

	[Header("Can only play Guard for 1 turn.\n After gain immunity to stun for 3 turns.")]
	STUN,

	[Header("Every start of turn +1 Hp for 3 turns.")]
	HEALTH_REGEN,

	[Header("Can only apply when having armour.\n Deal 3 damage when being attack.\n Last for 3 turns or until armour is destroyed.")]
	COUNTER,

	[Header("Immune to all status for 3 turns.")]
	RESISTANCE,

	[Header("Deal 3x damage instead of 2x when apply element weakness damage.\n Last for 1 turn.")]
	WEAKNESS_EXPLOIT,

	[Header("Remove all buff and debuff.")]
	NULLIFY_ALL,

	[Header("Every start of turn -1 Stamina, Last for 3 turns.\n After gain immunity to fatigue for 3 turns.")]
	FATIGUE,

	[Header("Removes element and prevent applying for element. Last 3 turns.")]
	ELEMENTLESS
}


[CreateAssetMenu(fileName = "New Action", menuName = "Underdog/ActionData" )]
public class ActionData : ScriptableObject {

	public ActionType actionType;
}
