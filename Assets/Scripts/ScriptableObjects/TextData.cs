using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//[CreateAssetMenu(fileName = "New Text Data", menuName = "Underdog/TextData" )]
public class TextData
{
	public string[] ActionString = {
		"Strike", "Heal", "Guard", "Boost",
		"Draw", "Reshuffle", "Discard", "Charge", "Enhance"
	};

	public string[] StatusString = {
		"Bleed","Poison", "Stun", "Health Regen",
		"Counter", "Resistance", "Weakness Exploit",
		"Nullify All", "Fatigue", "Elementless"
	};

	public string[] ElementString = { "None", "Dark", "Light", "Fire", "Earth", "Water" };

	public string[] ActionLongDesc =
	{
		"Deal _ Damage",	//Strike
		"Health +_", 	//Heal
		"Armour +_", 		//Guard
		"Apply _ element to character for this battle", //Boost
		"Draw _ cards", //Draw
		"Shuffle back _ card then draw _ card", //Reshuffle
		"Discard _ card", //Discard
		"Recover _ stamina", //Charge
		"If have a stronger element applied this deals _ element damage instead" //Enhance
	};

	public string[] StatusLongDesc = {
		"Chance to Bleed - 60%. For each card played, -1 Health for opponent. Last for 3 turns. After 3 turns, opponent gain immunity to bleed for 3 turns.",	//Bleed

		"Chance to Poison - 60%. At the start of a new turn, -3 Health for opponent. Last for 3 turns. After 3 turns, opponent gain immunity to poison for the next 3 turns.", 	//Poison

		"Chance to Stun - 60%. Can only play 'Guard' card for 1 turn for opponent. Last 1 turn. After 1 turn, opponent gain immunity to stun for 3 turns", 	//Stun

		"At the start of a new turn, +1 Health for 3 turns. Last 3 turn", //Health Regen

		"Can only apply when having armour. Deal 3 damage to opponent when being attack. Last for 3 turns or until armour is destroyed.", //Counter

		"Gain resistance to all negative status. Last for 3 turn", //Resistance

		"Deal 3x damage instead of 2x when apply element weakness damage. Last for 1 turn",	//Weakness Exploit

		"Remove all buff and debuff. Last for 1 turn", //Nullify All

		"At the start of a new turn, -1 Stamina for opponent. Last for 3 turns. After 3 turns, opponent gain immunity to fatigue for 3 turns.",	//Fatigue

		"Removes element and prevent applying for element. Last 3 turns."	// Elementless
	};
}
