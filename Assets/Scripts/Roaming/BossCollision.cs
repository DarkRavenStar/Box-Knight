using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossCollision : MonoBehaviour {
    public int myId;
    public int type;
    private int spawn;
    string bossID;

    private void Start()
    {
        PlayerPrefs.GetInt("bossType");
        spawn = PlayerPrefs.GetInt("bossType");
    }
    void Update()
    {
        bossID = SceneManager.GetActiveScene().name + "/boss" + myId;
        if (PlayerPrefs.GetInt(bossID) != 0)
        {
            gameObject.SetActive(false);
        }

        if (type == 0)
        {
            spawn = 1;
        }
        else if (type == 1)
        {
            spawn = 2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerModel"))
        {
            PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("BossBattleScene");
            PlayerPrefs.SetInt(bossID, 1);
            PlayerPrefs.SetInt("bossType", spawn);
        }
    }
}
