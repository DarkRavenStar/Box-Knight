using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum UpgradeType
{
	TIER_0,
	TIER_1,
	TIER_2,
	TIER_3
};


[System.Serializable]
public class CardParent : ScriptableObject
{
	public bool IsConditionCard = false;
	public new string name;
	public int staminaCost;
	public string description;
	public Sprite artwork;
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Card", menuName = "Underdog/CardData" )]
public class CardData : CardParent
{
	public UpgradeType upgradeType;
	public CardData upgradeCard;
	public ElementData element;
	public string fullDescription;
	public List<ActionCardEffect> actionEffect;
	public List<StatusCardEffect> statusEffect;
}
