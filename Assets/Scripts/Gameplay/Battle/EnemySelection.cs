using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelection : MonoBehaviour
{
    public GameObject Imp;
    public GameObject Bandaged;
    public GameObject Wizard;

    void Start()
    {
        if (PlayerPrefs.GetInt("enemyType") == 1)
        {
            Imp.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("enemyType") == 2)
        {
            Bandaged.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("enemyType") == 3)
        {
            Wizard.SetActive(true);
        }
    }
}
