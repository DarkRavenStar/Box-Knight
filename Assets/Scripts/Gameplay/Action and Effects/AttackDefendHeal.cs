using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDefendHeal : MonoBehaviour {
	
	bool isPlayer;
	private int DamageData;
	private int ArmourData;
	private int HealingData;

	public HealthCounter PlayerHP;
	public HealthCounter EnemyHP;

	public ArmourCounter PlayerArmour;
	public ArmourCounter EnemyArmour;

	public PlayerData PlayerData;
    public EnemyData enemyData;

	public ShowDamage PlayerDamage;
	public ShowDamage EnemyDamage;

	public ElementDmgCalculation ElementCalculation;

	void Start()
	{
		//PlayerData = BattleSceneManager.Instance.GetPlayerData();
        //enemyData = BattleSceneManager.Instance.enemyData;
    }

	public void GetCardData(ActionCardEffect actionEffect, ElementData element)
	{
		DamageData = 0;
		ArmourData = 0;
		HealingData = 0;

		if (actionEffect.actionType == ActionType.STRIKE)
		{
			DamageData += actionEffect.num;
		}
		if (actionEffect.actionType == ActionType.GUARD)
		{
			ArmourData += actionEffect.num;
		}
		if (actionEffect.actionType == ActionType.HEAL)
		{
			HealingData += actionEffect.num;
		}
		if(actionEffect.actionType == ActionType.BOOST)
		{
			//Boost Element (Add Element)
			if(isPlayer == true) ElementCalculation.PlayerCharge(element);
			if(isPlayer == true) ElementCalculation.EnemyCharge(element);
		}
		if(actionEffect.actionType == ActionType.ENHANCE)
		{
			//Calculates Damage with Element Resist/Weakness
			if(isPlayer == true) DamageData = ElementCalculation.PlayerAttackCalculate(DamageData, PlayerData, enemyData);
			if(isPlayer == false)DamageData = ElementCalculation.EnemyAttackCalculate(DamageData, enemyData, PlayerData);
		}
		PlayCardData();
	}

	public void PlayCardData()
	{
		if (DamageData > 0)
		{
			EnemyHP.HealthDecrease(DamageData);
		}
		if (ArmourData > 0)
		{
			PlayerArmour.ArmourIncrease(ArmourData);
		}
		if (HealingData > 0)
		{
			PlayerHP.HealthIncrease(HealingData, PlayerData.maxHealth);
		}
		ShowCardValue();
	}

	public void ShowCardValue()
	{
		if (DamageData > 0)
		{
			EnemyDamage.DamageText(DamageData);
		}
		if (ArmourData > 0)
		{
			PlayerDamage.DefendText(ArmourData);
		}
		if (HealingData > 0)
		{
			PlayerDamage.HealText(HealingData);
		}
	}
}
