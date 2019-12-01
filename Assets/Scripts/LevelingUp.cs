using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingUp : MonoBehaviour {

    public PlayerData Player;
    public float BaseEXPRequirement;
    public float EXPRequirement;

    private bool health;
    private bool stamina;

    public void Start()
    {
        health = true;
        stamina = false;
        BaseEXPRequirement = 100.0f;
		//Player = BattleSceneManager.Instance.GetPlayerData ();
    }

    public void SetRequiredEXP()
    {
        EXPRequirement = 100 * (Mathf.Pow(Player.level, 1.5f));
    }

    public void AddEnemyEXP()
    {
        if (PlayerPrefs.GetInt("enemyType") == 1)//Imp
        {
            Player.exp += 150.0f;
        }
        else if (PlayerPrefs.GetInt("enemyType") == 2)//BandageGuy
        {
            Player.exp += 250.0f;
        }
        else if (PlayerPrefs.GetInt("enemyType") == 3)//Merchant
        {
            Player.exp += 375.0f;
        }
        PlayerPrefs.SetInt("enemyType", 0);
        CheckEXP();
    }

    public void AddBossEXP()
    {
        if (PlayerPrefs.GetInt("bossType") == 1)//Golem
        {
            Player.exp += 550.0f;
        }
        else if (PlayerPrefs.GetInt("bossType") == 2)//DemonLord
        {
            Player.exp += 1000.0f;
        }
        PlayerPrefs.SetInt("bossType", 0);
        CheckEXP();
    }

    [ContextMenu("CheckLevel")]
    public void CheckEXP()
    {
        if(Player.exp >= EXPRequirement)
        {
            Player.level++;
            if(Player.level == 10)
            {
                Player.maxHealth++;
                Player.stamina++;
            }
            else if(Player.level == 15)
            {
                Player.maxHealth+=2;
                Player.stamina++;
            }
            else if(Player.level > 10 && Player.level < 15)
            {
                if(Player.level == 13)
                {
                    Player.stamina++;
                }
                Player.maxHealth++;
            }
            else
            {
                if (health == true)
                {
                    Player.maxHealth++;
                    health = false;
                    stamina = true;
                }
                if (stamina == true)
                {
                    Player.stamina++;
                    health = true;
                    stamina = false;
                }
            }
            Player.playerHealth++;
            ResetEXP();
            SetRequiredEXP();
        }
    }

    public void ResetEXP()
    {
        Player.exp = Player.exp - EXPRequirement;
    }
}
