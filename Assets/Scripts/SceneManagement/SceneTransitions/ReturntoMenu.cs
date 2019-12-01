using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturntoMenu : MonoBehaviour
{
    public float Timer;
	
	void Update ()
    {
		if(Timer <= 0)
        {
            SceneManager.LoadScene(0);
        }
        Countdown();
	}

    void Countdown()
    {
        Timer -= Time.deltaTime;
    }
}
