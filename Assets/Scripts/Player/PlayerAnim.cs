using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnim : MonoBehaviour {
    public Animator anim;

    public HealthCounter player;
    public HealthCounter enemy;

    int currentHealth;
    int enemyHealth;
    int playerArmor;

    private void Awake()
    {
		//player = BattleSceneManager.Instance.GetPlayerData ();
		//enemy = BattleSceneManager.Instance.enemyData;
		currentHealth = player.CurrentHealth;
        enemyHealth = enemy.CurrentHealth;
        playerArmor = player.ArmourData;
    }

    // Update is called once per frame
    void Update () {
        if(playerArmor > 0)
        {
            Block();
        }
        else
        {
            Hurt();
        }
        Attack();
        Dead();
	}

    void Dead()
    {
        if(currentHealth == 0)
        {
            anim.SetBool("isDead", true);
            anim.SetBool("isIdle", false);
        }
    }

    void Hurt()
    {
        if (player.CurrentHealth < currentHealth)
        {
            currentHealth = player.CurrentHealth;
            //anim.Play("Hurt");
            anim.SetBool("isHurt", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            //anim.Play("Idle");
            anim.SetBool("isIdle", true);
            anim.SetBool("isHurt", false);
        }
    }

    void Attack()
    {
        if(enemy.CurrentHealth < enemyHealth)
        {
            enemyHealth = enemy.CurrentHealth;
            //anim.Play("Attack");
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttack", true);
        }
        else 
        {
            // anim.Play("Idle");
            anim.SetBool("isAttack", false);
            anim.SetBool("isIdle", true);
        }
    }

    void Block()
    {
        if(player.ArmourData < playerArmor)
        {
            playerArmor = player.ArmourData;

            anim.SetBool("isBlock", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isBlock", false);
            anim.SetBool("isIdle", true);
        }
    }
}
