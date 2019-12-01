using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Underdog/PlayerData" )]
public class PlayerData : ScriptableObject {
	public int maxHealth;
	public int playerHealth;
	public int armor;
	public int stamina;
	public float exp;
	public int level;
    public ElementData elementType;
    public ElementData Fire;
    public ElementData Water;
    public ElementData Earth;
    public ElementData resetElement;
}
