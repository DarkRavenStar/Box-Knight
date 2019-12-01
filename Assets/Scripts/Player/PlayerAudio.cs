using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip attack;
    public AudioClip hurt;
    public AudioClip heal;
    public AudioClip armorBreak;
    public AudioClip block;
    public AudioClip BGM;

    public HealthCounter player;
    public HealthCounter enemy;

    int currentHealth;
    int playerArmor;
    int enemyHealth;

    void Start()
    {
        AudioManager.Instance.PlayMusic(BGM);
        currentHealth = player.CurrentHealth;
        enemyHealth = enemy.CurrentHealth;
        playerArmor = player.ArmourData;
    }
    // Update is called once per frame
    void Update()
    {
        if (player.ArmourData > 0)
        {
            Block();
        }
        else if (player.ArmourData <= 0)
        {
            Hurt();
        }
        Attack();
    }

    public void Attack()
    {
        if (enemy.CurrentHealth < enemyHealth)
        {
            enemyHealth = enemy.CurrentHealth;
            AudioManager.Instance.Play(attack);
        }
        //            Debug.Log("Hit!");
        //            AudioManager.Instance.StopAudio(attack);
    }

    void Hurt()
    {
        if (player.CurrentHealth < currentHealth)
        {
            //           AudioManager.Instance.
        }
    }

    void Block()
    {
        if (player.ArmourData < playerArmor)
        {
            AudioManager.Instance.Play(block);
        }
    }

    void Heal()
    {
        if (player.CurrentHealth > currentHealth)
        {
            AudioManager.Instance.Play(heal);
        }
    }
}
