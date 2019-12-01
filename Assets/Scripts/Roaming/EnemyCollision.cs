using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollision : MonoBehaviour
{
    public int myId;
    public int type;
    private int spawn;
    string enemyID;

    private void Start()
    {
        PlayerPrefs.GetInt("enemyType");
        spawn = PlayerPrefs.GetInt("enemyType");
    }
    void Update()
    {
        enemyID = SceneManager.GetActiveScene().name + "/enemy" + myId;
        if (PlayerPrefs.GetInt(enemyID) != 0)
        {
            Destroy(gameObject);
        }

        if (type == 0)
        {
            spawn = 1;
        }
        else if (type == 1)
        {
            spawn = 2;
        }
        else if (type == 2)
        {
            spawn = 3;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerModel"))
        {
            PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("BattleScene");
            PlayerPrefs.SetInt(enemyID, 1);
            PlayerPrefs.SetInt("enemyType", spawn);
        }
    }
}
