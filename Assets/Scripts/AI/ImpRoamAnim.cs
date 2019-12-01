using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpRoamAnim : MonoBehaviour {
    public Animator anim;
    private Rigidbody rb;
    float initRb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        initRb = rb.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(rb.position.x > initRb || rb.position.x < initRb)
        {
            initRb = rb.position.x;
            anim.SetBool("isWalk", true);
            anim.SetBool("isIdle", false);
        }
        else if( rb.position.x == initRb)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalk", false);
        }
	}
}
