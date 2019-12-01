using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsAppearance : MonoBehaviour {
    public GameObject DemonKing;
    public GameObject stairs;
	
	// Update is called once per frame
	void Update () {
		if(DemonKing.activeSelf == false)
        {
            stairs.SetActive(true);
        }
	}
}
