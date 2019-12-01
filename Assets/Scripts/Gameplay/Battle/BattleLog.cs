using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLog : MonoBehaviour {

	private static BattleLog _instance;

	public static BattleLog Instance 
	{ 
		get { return _instance; }
	} 

	private void Awake()
	{ 
		if (_instance != null && _instance != this) 
		{ 
			Destroy(this.gameObject);
			return;
		}

		_instance = this;
	}

	public ElementType elementType;
	public int playerHealth; //Done

	public int numberOfCardsUsed;
	public int numberOfTurns; //Done

	public int stamina; //Done
	public int staminaLimit; //Done
}
