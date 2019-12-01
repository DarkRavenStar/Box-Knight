using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSelection : MonoBehaviour
{
    public GameObject Golem;
    public GameObject DemonKing;

    void Start()
    {
        if (PlayerPrefs.GetInt("bossType") == 1)
        {
            Golem.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("bossType") == 2)
        {
            DemonKing.SetActive(true);
        }
    }
}
