using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private Level01EnemyCheck enemy;

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<Level01EnemyCheck>();
    }

    void OnTriggerEnter(Collider other)
    {
		if(other.CompareTag("EnemyModel"))
        {
            PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("BattleScene");
            //Debug.Log("Collided with " + enemy.myId);
            enemy.Die();
        }
    }
}
