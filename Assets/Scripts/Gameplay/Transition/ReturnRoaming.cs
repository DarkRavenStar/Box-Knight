using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnRoaming : MonoBehaviour
{
    public bool CardShown;
    public GameObject Card;
    public GameObject Reward;

    public void ShowCard()
    {
        CardShown = true;
        Reward.SetActive(false);
        Card.SetActive(true);
    }

    public void SwitchScene()
    {
        if (CardShown == true)
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("lastLoadedScene"));
        }
    }
}
