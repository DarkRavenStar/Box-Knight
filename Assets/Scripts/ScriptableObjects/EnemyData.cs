using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Enemy", menuName = "Underdog/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public ElementData elementType;
	public EnemyPatternData enemyPattern;
	public ElementData tempElement;
	public ElementData resetElement;
    public int maxHealth;
    public int enemyHealth;
    public int enemyArmour;
    public int staminaLimit; // Example imp only 5 staminaLimit to use for 3 attack damage and 2 defense and so on
    public int level;
}