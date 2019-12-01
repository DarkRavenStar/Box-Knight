using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject Player;

    private Vector3 Distance;

	// Use this for initialization
	void Start () {
        Distance = Player.transform.position - this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Player.transform.position - Distance; 
	}
}
