using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour {

	public DrawDiscardReshuffle deckSystem;
	[Header("Turn Switching")]
	public GameObject playerTurn;
	public GameObject enemyTurn;
	public Text turn;
	public Button endTurn;

	public int counter;
	[SerializeField] bool nextTurn;


	void Start ()
	{
		counter = 1;
		turn.text = "Round " + counter;
	}

	// Update is called once per frame
	void Update ()
	{
		if (BattleSceneManager.Instance.IsPlayerTurn == true)
		{
			if (BattleSceneManager.Instance.CanPlayerPlay == false)
			{
				endTurn.enabled = false;
			}

			if (BattleSceneManager.Instance.CanPlayerPlay == true)
			{
				endTurn.enabled = true;
			}
		}

		if (BattleSceneManager.Instance.IsPlayerTurn == false)
		{
			endTurn.enabled = false;
		}
	}

	[ContextMenu("EndPlayerTurn")]
	public void EndPlayerTurn()
	{
		playerTurn.SetActive (false);
		enemyTurn.SetActive (true);
		//BattleSceneManager.Instance.IsPlayerTurn = false;
		//BattleSceneManager.Instance.CanEnemyPlay = false;
	}

	[ContextMenu("EndEnemyTurn")]
	public void EndEnemyTurn()
	{
		playerTurn.SetActive (true);
		enemyTurn.SetActive (false);
		EndRound ();
		deckSystem.ActivateDraw ();
		BattleSceneManager.Instance.CanEnemyPlay = false;
		BattleSceneManager.Instance.CanPlayerPlay = false;
		BattleSceneManager.Instance.IsPlayerTurn = true;
	}

	public void EndRound()
	{
		counter++;
		turn.text = "Round " + counter;
	}
}
