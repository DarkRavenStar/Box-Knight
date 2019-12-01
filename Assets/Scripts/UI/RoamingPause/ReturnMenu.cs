using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour
{
    public RoamingPause roamingPause;

	public void Return()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1;
    }
}
