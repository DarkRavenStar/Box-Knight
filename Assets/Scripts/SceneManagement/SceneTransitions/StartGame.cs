using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public PlayerData Player;
    public DeckData PlayerDeck;

    public void BeginGame()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.DeleteAll();

        Player.maxHealth = 20;
        Player.playerHealth = Player.maxHealth;
        Player.stamina = 5;
        Player.exp = 0;
        Player.level = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
