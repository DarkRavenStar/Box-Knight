using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	public HealthBarUI healthbar;
	public Text healthText;
    public int playerHealth;
    public bool isHurt;

    public ArmourCounter ArmourValue;

    public PlayerData PlayerData;

    //UI Feedback
    public UISystem UIs;

    void Start()
    {
        playerHealth = PlayerData.playerHealth;
        isHurt = false;
		if (healthbar != null)
		{
			healthbar.min = 0;
			healthbar.max = PlayerData.playerHealth;
		}
    }

    void Update()
    {
        if(playerHealth <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }

        healthText.text = playerHealth.ToString();
    }

    public void HealthIncrease(int heals)
    {
        //UIs.HealAppear();
        if (playerHealth < 20)
        {
            playerHealth += heals;
        }
        if(playerHealth >= 19)
        {
            playerHealth = 20;
        }
        //healthbar.SetHealth (playerHealth);
            //UIs.HealAppear();//Not showing
    }

    public void HealthDecrease(int Damage)
    {
        isHurt = true;
        if (ArmourValue.Armour > 0)
        {
            int playerArmour = ArmourValue.Armour;
            ArmourValue.ArmourDecrease(Damage);
            Damage -= playerArmour;
            if (Damage < 0)
            {
                Damage = 0;
            }
        }
        playerHealth -= Damage;
		//healthbar.SetHealth (healthbar.CurrentValue - playerHealth);
        if (playerHealth >= 0)
        {
            healthText.text = playerHealth.ToString();
        }
        else
        {
            playerHealth = 0;
            healthText.text = playerHealth.ToString();
        }
        UIs.AttackAppear();

        PlayerData.playerHealth = playerHealth;
    }
}
