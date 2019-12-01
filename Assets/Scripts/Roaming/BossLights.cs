using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLights : MonoBehaviour {
    public BossIntro lights;
	
	void Start ()
    {
        transform.GetChild(0).gameObject.SetActive(false);
	}
	
	void Update ()
    {
		if(lights.entered == true)
        {
            transform.GetChild(0).gameObject.SetActive(true); 
        }
	}
}
