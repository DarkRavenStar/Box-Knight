using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level01EnemyCheck : MonoBehaviour
{
    public int myId;
    string playerPrefsString;
    void Update()
    {
        playerPrefsString = SceneManager.GetActiveScene().name + "/" + myId;
        if (PlayerPrefs.GetInt(playerPrefsString) != 0)
        {
            Destroy(gameObject);
        }
    }
    public void Die()
    {
        PlayerPrefs.SetInt(playerPrefsString, 1);
    }
}
