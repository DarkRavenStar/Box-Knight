using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour {

    public Image RedHPBar;
    public Image OrangeHPBar;

    public float Speed;

    public HealthCounter HP;

    public PlayerData PlayerStats;
    public EnemyData EnemyStats;

    public float MaxHealth;
    public float CurrentHealth;

    public bool IsPlayer;

	// Use this for initialization
	void Start () {
		if(IsPlayer == true && BattleSceneManager.Instance.GetPlayerData() == null)
        {
            MaxHealth = PlayerStats.maxHealth;
            CurrentHealth = PlayerStats.playerHealth;
        }
        
		if(IsPlayer == true && BattleSceneManager.Instance.GetPlayerData() != null)
		{
			MaxHealth = BattleSceneManager.Instance.GetPlayerData().maxHealth;
			CurrentHealth = BattleSceneManager.Instance.GetPlayerData().playerHealth;
		}

		if(IsPlayer == false && BattleSceneManager.Instance.GetPlayerData() == null)
        {
            MaxHealth = EnemyStats.maxHealth;
            CurrentHealth = HP.CurrentHealth;
        }

		if(IsPlayer == false && BattleSceneManager.Instance.GetPlayerData() != null)
		{
			MaxHealth = BattleSceneManager.Instance.enemyData.maxHealth;
			CurrentHealth = HP.CurrentHealth;
		}
	}

    public void SetHealth()
    {
        CurrentHealth = HP.CurrentHealth;
    }

	[ContextMenu("SetRedHealth")]
    public void setRedHP()
    {
        RedHPBar.fillAmount = CurrentHealth / MaxHealth;
    }

    public void setOrangeHP()
    {
        if(OrangeHPBar.fillAmount != RedHPBar.fillAmount)
        {
            if (OrangeHPBar.fillAmount > RedHPBar.fillAmount)
            {
                OrangeHPBar.fillAmount -= Speed;
            }
            else if(OrangeHPBar.fillAmount < RedHPBar.fillAmount)
            {
                OrangeHPBar.fillAmount = RedHPBar.fillAmount;
            }
        }
    }

    // Update is called once per frame
    void Update () {
        SetHealth();
        setRedHP();
        setOrangeHP();
	}
}
