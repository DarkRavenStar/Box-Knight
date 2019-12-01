using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToDeck : MonoBehaviour
{
	public void Return()
    {
        SceneManager.LoadScene("DeckBuildingScene");
        Time.timeScale = 1;
    }
}
