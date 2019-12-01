using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingPause : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        if(paused == false)
        {
            paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
