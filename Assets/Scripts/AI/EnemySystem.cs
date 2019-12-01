using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour {

	public TurnSystem turnSystem;
	public EnemyPatternData enemy;

	[Header("Status Effect System")]
	public StatusSystem playerStatusSystem;
	public StatusSystem enemyStatusSystem;

	[Header("AttackDefendHeal System")]
	public AttackDefendHeal Action;

	[Header("PlayerElement UI System")]
	public ElementCall EnemyElement;

	WaitForSeconds awaitSecond = new WaitForSeconds(1f);

	public List<EnemyPattern> enemyPattern;
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

	public int patternNum = 0;
	bool isUsingElement = false;

	// Use this for initialization
	void Start () {
		//enemy = BattleSceneManager.Instance.enemyAttackPattern;
	}
	
	void Update()
	{
		if (BattleSceneManager.Instance.IsPlayerTurn == false)
		{

			if (BattleSceneManager.Instance.CanEnemyPlay == false)
			{
				
			}

			if (BattleSceneManager.Instance.CanEnemyPlay == true)
			{
				if (canStartActionEffect == true)
				{
					ActionEffectExecute ();
				}

				if (canStartActionEffect == false && canStartStatusEffect == true)
				{
					//StatusEffectExecute ();
				}
			}
		}
	}
	[ContextMenu("EnemyActionListing")]
	public void EnemyActionListing ()
	{
		if (patternNum == enemy.enemyTurn.Count) {
			patternNum = 0;
		}
		if (patternNum < enemy.enemyTurn.Count) {

			actionCardEffect.Clear ();
			statusCardEffect.Clear ();

			int patternCount = enemy.enemyTurn [patternNum].enemyPattern.Count;
			enemyPattern = enemy.enemyTurn [patternNum].enemyPattern;

			for (int i = 0; i < patternCount; i++) {
				if (enemyPattern [i].UseAlternativePattern == false) {
					Pattern pattern = enemyPattern [i].pattern;
					if (pattern.patternType == PatternType.ACTION) {
						actionCardEffect.Add (pattern.actionEffect);
						if (pattern.actionEffect.actionType == ActionType.BOOST) {
							elementData = pattern.elementType;
						}
					}

					if (pattern.patternType == PatternType.STATUS) {
						statusCardEffect.Add (pattern.statusEffect);
					}
				}

				if (enemyPattern [i].UseAlternativePattern == true) {
					Pattern pattern = enemyPattern [i].alternativePattern;
					if(isUsingElement == false) pattern = enemyPattern [i].pattern;
					if(isUsingElement == true) pattern = enemyPattern [i].alternativePattern;
					if (pattern.patternType == PatternType.ACTION) {
						actionCardEffect.Add (pattern.actionEffect);
						if (pattern.actionEffect.actionType == ActionType.BOOST) {
							elementData = pattern.elementType;
						}
					}

					if (pattern.patternType == PatternType.STATUS) {
						statusCardEffect.Add (pattern.statusEffect);
					}
				}
			}

			actionCount = 0;
			actionSize = actionCardEffect.Count;
			if (actionSize > 0 && actionCardEffect != null) {
				canStartActionEffect = true;
			}

			statusCount = 0;
			statusSize = statusCardEffect.Count;
			if (statusSize > 0 && statusCardEffect != null) {
				canStartStatusEffect = true;
			}
			patternNum++;
		}
	}

	public void ActionEffectExecute()
	{
		if (actionCount < actionSize && actionCardEffect.Count > 0)
		{
			tempAction = actionCardEffect [actionCount];

			if (UseGuardOnly == false)
			{
				if (tempAction.actionType == ActionType.BOOST)
				{
					if (elementData.elements == ElementType.NONE)
						isUsingElement = false;
					else
						isUsingElement = true;
					if(elementData.elements == ElementType.EARTH) EnemyElement.CallEarth ();
					if(elementData.elements == ElementType.WATER) EnemyElement.CallWater ();
					if(elementData.elements == ElementType.FIRE)  EnemyElement.CallFire ();

					Action.GetCardData (actionCardEffect [actionCount], elementData);
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
            enemyStatusSystem.StatusEffects();
			turnSystem.EndEnemyTurn ();
			canStartActionEffect = false;
		}
  	}
}
