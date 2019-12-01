using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonTaunt : MonoBehaviour {
    public GameObject taunt;
    private float timer = 3.0f;
	
	void Start ()
    {
        taunt.SetActive(false);
	}

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("PlayerModel"))
        {
            taunt.SetActive(true);
        }
    }

    private void Update()
    {
        if(taunt.activeSelf == true)
        {
            timer -= 1 * Time.deltaTime;
        }
        if(timer <= 0)
        {
            taunt.SetActive(false);
            timer = 3.0f;
        }
    }
}
