using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour {
    public  Animator anim;

    public HealthCounter player;
    public HealthCounter enemy;

    int currentHealth;
    int playerHealth;
    int playerArmor;

    private void Start()
    {
        currentHealth = enemy.CurrentHealth;
        playerHealth = player.CurrentHealth;
        playerArmor = player.ArmourData;
    }

    void Update()
    {
        Hurt();
        Attack();
        Dead();
    }

    void Dead()
    {
		if(enemy.CurrentHealth <= 0)
        {
            anim.SetBool("isDead", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isDead", false);
            anim.SetBool("isIdle", true);
        }
    }
    void Attack()
    {
        if(player.CurrentHealth < playerHealth)
        {
            playerHealth = player.CurrentHealth;
            anim.SetBool("isAttack", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isAttack", false);
            anim.SetBool("isIdle", true);
        }
        
    }
    void Hurt()
    {
        if(enemy.CurrentHealth < currentHealth)
        {
            currentHealth = enemy.CurrentHealth;
            anim.SetBool("isHurt", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isHurt", false);
            anim.SetBool("isIdle", true);
        }
            
    }
}
