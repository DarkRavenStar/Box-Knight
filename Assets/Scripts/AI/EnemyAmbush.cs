using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmbush : MonoBehaviour {

    public Transform raycastPoint;
    public LayerMask layer;

	// Update is called once per frame
	void Update () {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(raycastPoint.position, Vector3.left, 3f, layer);

        if(hit.transform != null)
        {
            if(hit.transform.CompareTag("Player") == true)
            {
                Debug.Log("Oof!");
                this.transform.Translate(Vector3.left * Time.deltaTime);
            }
        }
	}
}
