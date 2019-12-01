using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthCounter : MonoBehaviour
{
    public int CurrentHealth;
    public ArmourCounter CurrentArmour;
    public Text healthText;
    int DamageData;
    public bool isDamage = false;
    public GameObject winScreen;
    public EnemyData enemyData;
    public PlayerData playerData;

    public bool IsPlayer;
    public bool isHurt;
    public int ArmourData;

    //UI Feedback
    public UISystem UIs;

    public void Start()
    {
        enemyData = BattleSceneManager.Instance.enemyData;
        //playerData = BattleSceneManager.Instance.GetPlayerData ();
        if (IsPlayer == false)
        {
            CurrentHealth = enemyData.maxHealth;
            healthText.text = CurrentHealth.ToString();
        }
        else if (IsPlayer == true)
        {
            CurrentHealth = playerData.playerHealth;
            healthText.text = CurrentHealth.ToString();
        }
        winScreen.SetActive(false);
    }

    public void HealthDecrease(int Damage)
    {
        if (Damage > 0)
        {
            if (CurrentArmour.Armour > 0)
            {
                ArmourData = CurrentArmour.Armour;
                CurrentArmour.ArmourDecrease(Damage);
                Damage -= ArmourData;
                if (Damage < 0)
                {
                    Damage = 0;
                }
            }
            CurrentHealth -= Damage;
            isDamage = true;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
            UIs.AttackAppear();
            UpdateHealth();
            checkHealth();
        }
    }

    public void HealthIncrease(int Heals, int MaxHP)
    {
        if (Heals > 0)
        {
            CurrentHealth += Heals;

            if (CurrentHealth > MaxHP)
            {
                CurrentHealth = MaxHP;
            }

            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
            UpdateHealth();
            UIs.HealAppear();
        }
    }

    public void UpdateHealth()
    {
        healthText.text = CurrentHealth.ToString();
    }

    public void checkHealth()
    {
        if(CurrentHealth <= 0)
        {
            winScreen.SetActive(true);
        }
    }
}
