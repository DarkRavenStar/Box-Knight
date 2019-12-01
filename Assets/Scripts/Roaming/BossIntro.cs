using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIntro : MonoBehaviour {
    public ThirdPersonCamera camDistance;
    public bool entered = false;
    public GameObject block;

    private void Start()
    {
        block.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerModel")
        {
            entered = true;
            Debug.Log("Boss Fight!");
        }
    }

    private void Update()
    {
        if (entered == true)
        {
            if (camDistance.distance < 20) 
            {
                camDistance.distance++;
                block.SetActive(true);
            }
        }
    }
}
