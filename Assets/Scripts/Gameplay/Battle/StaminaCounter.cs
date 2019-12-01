using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaCounter : MonoBehaviour
{

    public int MaxStamina;
    public int currentStamina;
    public Text staminaText;
    int StaminaData;

    public PlayerData Player;

    public void Start()
    {
		//Player = BattleSceneManager.Instance.GetPlayerData ();
        MaxStamina = Player.stamina;
        currentStamina = MaxStamina;
        staminaText.text = currentStamina.ToString();
    }

    public void StaminaIncrease(int Stamina)
    {
        currentStamina += Stamina;
        UpdateStamina();
    }

    public void StaminaDecrease(int Stamina)
    {
        currentStamina -= Stamina;
        
        if(currentStamina < 0)
        {
            currentStamina = 0;
        }
        UpdateStamina();
    }

    public void IncreaseMax()
    {
        if(MaxStamina < 10)
        {
            MaxStamina += 1;
        }
    }

    public void Replenish()
    {
        currentStamina = MaxStamina;
        staminaText.text = currentStamina.ToString();
    }

    public void UpdateStamina()
    {
        staminaText.text = currentStamina.ToString();
    }
}
