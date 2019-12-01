using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnInvisible : MonoBehaviour
{
	public float Timer = 3.0f;

	void Start()
	{
		gameObject.SetActive(true);
	}

	// Update is called once per frame
	void Update ()
	{
		if(BattleSceneManager.Instance.CanPlayerPlay == false)
		{
			gameObject.SetActive(false);
		}
		else if(Timer <= 0.0f)
		{
			gameObject.SetActive(false);
		}

		Countdown();
	}

	void Countdown()
	{
		if(gameObject.activeSelf == true)
		{
			Timer -= Time.deltaTime;
		}
		else
		{
			Timer = 3.0f;
		}
	}
}
