using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {

	public Image chipOffBar;
	public Image trueHealthBar;
	public Text healthText;

	public int min;
	public int max;
	private int currentValue;
	private float currentPercent;
	[Range(0, 15)]
	public int range = 0;

	void Update()
	{
		SetHealth (range);
	}

	public void SetHealth(int health)
	{
		if (health != currentValue)
		{
			if (max - min == 0) {
				currentValue = 0;
				currentPercent = 0;
			} else {
				currentValue = health;
				currentPercent = (float)currentValue / (float)(max - min);
			}

			healthText.text = string.Format("{0}", Mathf.RoundToInt(currentPercent * 100));

			trueHealthBar.fillAmount = currentPercent;
		}
	}

	public float CurrentPercent
	{
		get{ return currentPercent; }
	}

	public int CurrentValue
	{
		get{ return currentValue; }
	}
}
