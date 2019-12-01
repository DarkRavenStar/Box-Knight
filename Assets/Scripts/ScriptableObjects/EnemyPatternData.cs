using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PatternType
{
	ACTION,
	STATUS,
}

[System.Serializable]
public class Pattern
{
	public PatternType patternType;
	public ActionCardEffect actionEffect;
	public StatusCardEffect statusEffect;
	public ElementData elementType;
}


[System.Serializable]
public class EnemyPattern
{
	public bool UseAlternativePattern;
	public Pattern pattern;
	public Pattern alternativePattern;

}

[System.Serializable]
public class EnemyTurn
{
	public List<EnemyPattern> enemyPattern;
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Enemy Pattern Data", menuName = "Underdog/EnemyPatternData" )]
public class EnemyPatternData : ScriptableObject
{
	public List<EnemyTurn> enemyTurn;
}
