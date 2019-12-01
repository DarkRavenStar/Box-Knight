using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfo : MonoBehaviour {

    public Slider healthBar;
    public PlayerHealth health;

	// Use this for initialization
	void Start () {
        healthBar.value = CalculateHealth();
	}
	
    float CalculateHealth()
    {
        return health.playerHealth / 20;
    }

	// Update is called once per frame
	void Update () {
        healthBar.value = CalculateHealth();
	}
}
