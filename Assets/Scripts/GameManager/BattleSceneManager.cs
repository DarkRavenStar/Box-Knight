using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour {

	private static BattleSceneManager _instance;

	public bool IsEnemyDone;
	public bool IsPlayerDone;

	public bool IsPlayerTurn;
	public bool CanPlayerPlay;
	public bool CanEnemyPlay;
	[SerializeField] PlayerData playerDataRef;

	[SerializeField] EnemyData imp;
	[SerializeField] EnemyData golem;
	[SerializeField] EnemyData wizard;
	[SerializeField] EnemyData bandageGuy;
	[SerializeField] EnemyData demonKing;

	[SerializeField] DeckData deckDataRef;
	[SerializeField] DrawDiscardReshuffle deckSystem;

	[SerializeField] DeckData playerDeck;

	[SerializeField] PlayerData playerData;
	public EnemyData enemyData;
	public EnemyPatternData enemyAttackPattern;

	public static BattleSceneManager Instance 
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

	void Start()
	{
        if (playerData == null)
		{
			SetPlayerData (playerDataRef, false);
		}
		if (playerData != null)
		{
			SetPlayerData (playerDataRef, true);
		}

		if (playerDeck == null)
		{
			SetDeckData (deckDataRef, false);
			deckSystem.PlayerDeck = GetDeck ();
		}

        if (PlayerPrefs.GetInt("enemyType") == 1)
        {
            enemyData = imp;
            enemyAttackPattern = imp.enemyPattern;
        }
        else if (PlayerPrefs.GetInt("enemyType") == 2)
        {
            enemyData = bandageGuy;
            enemyAttackPattern = bandageGuy.enemyPattern;
        }
        else if (PlayerPrefs.GetInt("enemyType") == 3)
        {
            enemyData = wizard;
            enemyAttackPattern = wizard.enemyPattern;
        }
        if (PlayerPrefs.GetInt("bossType") == 1)
        {
            enemyData = golem;
            enemyAttackPattern = golem.enemyPattern;
        }
        else if (PlayerPrefs.GetInt("bossType") == 2)
        {
            enemyData = demonKing;
            enemyAttackPattern = demonKing.enemyPattern;
        }          

        if (playerDeck != null)
		{
			SetDeckData (deckDataRef, true);
			deckSystem.PlayerDeck = GetDeck ();
		}

		if (IsPlayerTurn == true) {
			deckSystem.ActivateDrawFunction ();
		}
	}

	public void SetPlayerData(PlayerData pData, bool overwrite)
	{
		if (overwrite == true)
		{
			playerData = ScriptableObject.Instantiate (pData) as PlayerData; //CreateInstance<PlayerData> ();
		}
		else
		{
			playerData = ScriptableObject.Instantiate (pData) as PlayerData; //playerData = ScriptableObject.CreateInstance<PlayerData> ();
		}
	}

	public PlayerData GetPlayerData()
	{
		if (playerData != null)
			return playerData;
		else
			return null;
	}

	public void SetDeckData(DeckData deck, bool overwrite)
	{
		if (overwrite == true)
		{
			playerDeck = ScriptableObject.Instantiate (deck) as DeckData; //CreateInstance<PlayerData> ();
		}
		else
		{
			playerDeck = ScriptableObject.Instantiate (deck) as DeckData; //playerData = ScriptableObject.CreateInstance<PlayerData> ();
		}
	}

	public DeckData GetDeck()
	{
		if (playerDeck != null) {
			return playerDeck;
		} else {
			return null;
		}

	}

}
